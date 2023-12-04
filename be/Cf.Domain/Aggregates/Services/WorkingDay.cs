
namespace Cf.Domain.Aggregates.Services
{
    public class WorkingDay: Entity
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string LunchBreakStartTime { get; set; }
        public string LunchBreakEndTime { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
