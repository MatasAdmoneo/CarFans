using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cf.Domain.Models
{
    public class ServiceWorkingHours
    {
        public required DayOfWeek DayOfWeek { get; set; }
        public required string StartTime { get; set; }
        public required string EndTime { get; set; }
        public required string LunchBreakStartTime { get; set; }
        public required string LunchBreakEndTime { get; set; }
    }
}
