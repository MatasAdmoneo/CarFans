using Cf.Application.Services.Interfaces;
using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Services;
using Cf.Domain.Enums;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Exceptions;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Cf.Contracts.Mappers;
using Cf.Contracts.Extensions.ServiceExtensions;

namespace Cf.Application.Services.ServiceInformation
{
    public class ServiceInfoService : IServicelnfoService
    {
        private readonly Context _context;
        private readonly IServiceWorkingDaysService _serviceWorkingDaysService;

        public ServiceInfoService(Context context, IServiceWorkingDaysService serviceWorkingDaysService)
        {
            _context = context;
            _serviceWorkingDaysService = serviceWorkingDaysService;
        }

        public async Task AddAsync(string? serviceId, ServiceAdditionalInfoModel additionalInfo)
        {
            if (serviceId is null)
                throw new ApplicationException();

            ValidateAdditionalInfo(additionalInfo);

            var service = new Service(serviceId, ServiceStatus.CreatedInDataBase, additionalInfo.ServiceName!, additionalInfo.City!, additionalInfo.Address!, null!, additionalInfo.ContactPhone!, additionalInfo.Description);

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            await _serviceWorkingDaysService.CreateAsync(service.Id, additionalInfo.WeeklyWorkingHours!);
        }

        public async Task UpdateAsync(string? serviceId, ServiceAdditionalInfoModel additionalInfo)
        {
            if (serviceId is null || additionalInfo is null)
                throw new ApplicationException();

            var service = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);

            if (service is null)
                throw new NotFoundException(DomainErrors.Service.NotFound);

            if (additionalInfo.WeeklyWorkingHours is not null)
            {
                ValidateWeeklyWorkingHoursFormat(additionalInfo.WeeklyWorkingHours);
                await _serviceWorkingDaysService.CreateAsync(service.Id, additionalInfo.WeeklyWorkingHours!);
            }

            service.UpdateFields(additionalInfo);

            await _context.SaveChangesAsync();
        }

        public async Task<Response.ServiceAdditionalFields> GetByServiceIdAsync(string? serviceId)
        {
            if (serviceId is null)
                throw new ApplicationException();

            var service = await _context.Services
                .Include(x => x.WeeklyWorkingHours)
                .FirstOrDefaultAsync(x => x.ServiceId == serviceId);

            if (service == null)
                return new Response.ServiceAdditionalFields(null, null, null, null, null, null);

            return service.ToServiceInfoModel();
        }

        public async Task<Response.ServiceStatusInfo> GetStatusByIdAsync(string? serviceId)
        {
            if (serviceId is null)
                throw new ApplicationException();

            var service = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);

            if (service == null)
                return new Response.ServiceStatusInfo(ServiceStatus.Exists);

            return new Response.ServiceStatusInfo(service.Status);
        }

        private void ValidateWeeklyWorkingHoursFormat(List<ServiceWorkingHours>? weeklyWorkingHours)
        {
            if (weeklyWorkingHours == null)
                throw new BadRequestException(DomainErrors.Service.InvalidFormatUnspecified);

            foreach (var workingDay in weeklyWorkingHours)
            {
                ValidateTimeFormat(workingDay.StartTime);
                ValidateTimeFormat(workingDay.EndTime);
                ValidateTimeFormat(workingDay.LunchBreakStartTime);
                ValidateTimeFormat(workingDay.LunchBreakEndTime);
            }
        }

        private void ValidateTimeFormat(string? timeString)
        {
            // Check if the string is null or empty
            if (string.IsNullOrEmpty(timeString))
            {
                throw new BadRequestException(DomainErrors.Service.InvalidFormatUnspecified);
            }

            // Split the time string into hours and minutes
            string[] timeParts = timeString.Split(':');

            if (timeParts.Length != 2)
            {
                throw new BadRequestException(DomainErrors.Service.InvalidFormatUnspecified);
            }

            // Ensure hours and minutes are valid integers
            if (!int.TryParse(timeParts[0], out int hours) || !int.TryParse(timeParts[1], out int minutes))
            {
                throw new BadRequestException(DomainErrors.Service.InvalidFormatUnspecified);
            }

            // Check if hours and minutes are within valid ranges
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59)
            {
                throw new BadRequestException(DomainErrors.Service.InvalidFormatUnspecified);
            }
        }

        private void ValidateAdditionalInfo(ServiceAdditionalInfoModel additionalInfo)
        {
            ValidateWeeklyWorkingHoursFormat(additionalInfo.WeeklyWorkingHours);

            if (additionalInfo == null ||
                additionalInfo.ServiceName == null ||
                additionalInfo.City == null ||
                additionalInfo.Address == null ||
                additionalInfo.WeeklyWorkingHours == null ||
                additionalInfo.ContactPhone == null)
                throw new BadRequestException(DomainErrors.Service.FieldsMissing);
        }
    }
}
