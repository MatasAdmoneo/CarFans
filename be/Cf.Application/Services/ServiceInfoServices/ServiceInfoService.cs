using Cf.Application.Services.Interfaces;
using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Services;
using Cf.Domain.Enums;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Exceptions;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cf.Contracts.Mappers;

namespace Cf.Application.Services.ServiceInfoServices
{
    public class ServiceInfoService : IServicelnfoService
    {
        private readonly Context _context;

        public ServiceInfoService(Context context)
        {
            _context = context;
        }

        public async Task UpdateInfoAsync(string? serviceId, ServiceAdditionalInfoModel additionalInfo)
        {
            if (serviceId is null)
                throw new ApplicationException();

            // Validate WeeklyWorkingHours format
            if (additionalInfo.WeeklyWorkingHours != null)
            {
                var isTimeFormatValid = ValidateWeeklyWorkingHoursFormat(additionalInfo.WeeklyWorkingHours);
                if (!isTimeFormatValid)
                    throw new BadRequestException(DomainErrors.Service.InvalidFormatUnspecified);
            }

            var service = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);

            if (service is null)
            {
                // Ensure that all fields in additionalInfo are not null before creating a new service
                if (additionalInfo != null &&
                    additionalInfo.ServiceName != null &&
                    additionalInfo.City != null &&
                    additionalInfo.Adress != null &&
                    additionalInfo.WeeklyWorkingHours != null &&
                    additionalInfo.ContactPhone != null)
                {
                    var newService = new Service(
                        serviceId,
                        ServiceStatus.CreatedInDataBase,
                        additionalInfo.ServiceName,
                        additionalInfo.City,
                        additionalInfo.Adress,
                        additionalInfo.WeeklyWorkingHours,
                        additionalInfo.ContactPhone,
                        additionalInfo.Description);

                    // Set any other properties as needed

                    // Add the new service to the context and mark it as Added
                    _context.Services.Add(newService);

                    // Persist the changes to the database
                    await _context.SaveChangesAsync();
                    return;
                }
                else
                {
                    throw new BadRequestException(DomainErrors.Service.FieldsMissing);
                }
            }

            service.ServiceName = additionalInfo.ServiceName != null ? additionalInfo.ServiceName : service.ServiceName; ;
            service.Adress = additionalInfo.Adress != null ? additionalInfo.Adress : service.Adress; ;
            service.City = additionalInfo.City != null ? additionalInfo.City : service.City; ;
            service.ContactPhone = additionalInfo.ContactPhone != null ? additionalInfo.ContactPhone : service.ContactPhone; ;
            service.Description = additionalInfo.Description != null ? additionalInfo.Description : service.Description;
            
            if (additionalInfo != null)
            {
                service.UpdatedDate = DateTime.UtcNow;

                // Ensure that Entity Framework tracks the changes and persists them to the database
                _context.Entry(service).State = EntityState.Modified;

                // Update the entity in the database
                await _context.SaveChangesAsync();
            }
            // If no info was provided, just return
            return;
        }

        private bool ValidateWeeklyWorkingHoursFormat(List<Service.WorkingDay> weeklyWorkingHours)
        {
            foreach (var workingDay in weeklyWorkingHours)
            {
                if (!IsTimeFormatValid(workingDay.StartTime) || !IsTimeFormatValid(workingDay.EndTime)
                    || !IsTimeFormatValid(workingDay.LunchBreakStartTime) || !IsTimeFormatValid(workingDay.LunchBreakEndTime))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsTimeFormatValid(string timeString)
        {
            // Check if the string is null or empty
            if (string.IsNullOrEmpty(timeString))
            {
                return false;
            }

            // Split the time string into hours and minutes
            string[] timeParts = timeString.Split(':');

            if (timeParts.Length != 2)
            {
                return false; // Time format should be HH:mm
            }

            // Ensure hours and minutes are valid integers
            if (!int.TryParse(timeParts[0], out int hours) || !int.TryParse(timeParts[1], out int minutes))
            {
                return false;
            }

            // Check if hours and minutes are within valid ranges
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59)
            {
                return false;
            }

            // Additional custom checks specific to your requirements can be added here

            return true; // Format is valid
        }

        public async Task<Response.ServiceAdditionalFields> GetByServiceIdAsync(string? serviceId)
        {
            if (serviceId is null)
                throw new ApplicationException();

            var service = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);

            if (service == null)
                throw new NotFoundException(DomainErrors.Service.NotFound);

            if (service.ServiceId != serviceId)
                throw new ApplicationException();

            return service.ToServiceInfoModel();
        }
    }
}
