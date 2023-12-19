
namespace Cf.Domain.Models;

public class ServiceWorkingHours
{
    public int DayOfWeek { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public string? LunchBreakStartTime { get; set; }

    public string? LunchBreakEndTime { get; set; }
}

