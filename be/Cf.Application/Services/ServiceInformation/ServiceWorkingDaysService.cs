using Cf.Application.Services.Interfaces;
using Cf.Domain.Aggregates.Services;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cf.Application.Services.ServiceInformation
{
    public class ServiceWorkingDaysService : IServiceWorkingDaysService
    {
        private readonly Context _context;

        public ServiceWorkingDaysService(Context context)
        {
            _context = context;
        }
        public async Task CreateAsync(Guid serviceId, List<ServiceWorkingHours> serviceWorkingHours)
        {
            await RemoveByIdAsync(serviceId);

            var workingDays = serviceWorkingHours.Select(x => new WorkingDay
            {
                DayOfWeek = (DayOfWeek)x.DayOfWeek,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                LunchBreakStartTime = x.LunchBreakStartTime,
                LunchBreakEndTime = x.LunchBreakEndTime,
                ServiceId = serviceId
            }).ToList();

            await _context.AddRangeAsync(workingDays);
            await _context.SaveChangesAsync();
        }

        private async Task RemoveByIdAsync(Guid serviceId)
        {
            var existingWorkingDays = _context.WorkingDays.Where(wd => wd.ServiceId == serviceId).ToList();
            _context.WorkingDays.RemoveRange(existingWorkingDays);
            await _context.SaveChangesAsync();
        }
    }
}
