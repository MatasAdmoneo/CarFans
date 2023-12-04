using Cf.Application.Services.Interfaces;
using Cf.Domain.Aggregates.Services;
using Cf.Domain.Models;
using Cf.Infrastructure;

namespace Cf.Application.Services.ServiceInfoServices
{
    public class ServiceWorkingDaysService : IServiceWorkingDaysService
    {
        private readonly Context _context;

        public ServiceWorkingDaysService(Context context)
        {
            _context = context;
        }
        public void AddWorkingDaysByServiceId(Guid serviceId, List<ServiceWorkingHours> serviceWorkingHours)
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
                    ServiceId = serviceId // Set the ServiceId
                };

                workingDays.Add(workingDay);
            }
            _context.WorkingDays.AddRange(workingDays);
        }
    }
}
