using Cf.Application.Services.Interfaces;
using Cf.Domain.Aggregates.Services;
using Cf.Domain.Models;
using Cf.Infrastructure;

namespace Cf.Application.Services.ServiceInformation
{
    public class ServiceWorkingDaysService : IServiceWorkingDaysService
    {
        private readonly Context _context;

        public ServiceWorkingDaysService(Context context)
        {
            _context = context;
        }
        public Task<List<WorkingDay>> AddByServiceId(Guid serviceId, List<ServiceWorkingHours> serviceWorkingHours)
        {
            List<WorkingDay> workingDays = new List<WorkingDay>();

            foreach (var day in serviceWorkingHours)
            {
                var workingDay = new WorkingDay
                {
                    DayOfWeek = day.DayOfWeek,
                    StartTime = day.StartTime,
                    EndTime = day.EndTime,
                    LunchBreakStartTime = day.LunchBreakStartTime,
                    LunchBreakEndTime = day.LunchBreakEndTime,
                    ServiceId = serviceId
                };

                workingDays.Add(workingDay);
                _context.Add(workingDay);
            }
            return Task.FromResult(workingDays);
        }

        public void RemoveByServiceId(Guid serviceId)
        {
            var existingWorkingDays = _context.WorkingDays.Where(wd => wd.ServiceId == serviceId).ToList();
            _context.WorkingDays.RemoveRange(existingWorkingDays);
        }
    }
}
