using static Cf.Domain.Aggregates.Services.Service;

namespace Cf.Domain.Models
{
    public class ServiceAdditionalInfoModel
    {
        public required string ServiceName { get; set; }
        public required string Adress { get; set; }
        public required string City { get; set; }
        public required List<WorkingDay> WeeklyWorkingHours { get; set; }
        public required string ContactPhone { get; set; }
        public string? Description { get; set; }
    }
}
