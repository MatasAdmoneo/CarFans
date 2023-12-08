
namespace Cf.Domain.Models
{
    public class ServiceWorkingHours
    {
        public required int DayOfWeek { get; set; }
        public required string StartTime { get; set; }
        public required string EndTime { get; set; }
        public required string LunchBreakStartTime { get; set; }
        public required string LunchBreakEndTime { get; set; }
    }
}
