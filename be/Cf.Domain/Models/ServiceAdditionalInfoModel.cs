using static Cf.Domain.Aggregates.Services.Service;

namespace Cf.Domain.Models
{
    public class ServiceAdditionalInfoModel
    {
        public string? ServiceName { get; set; }
        public string? Adress { get; set; }
        public string? City { get; set; }
        public List<WorkingDay>? WeeklyWorkingHours { get; set; }
        public string? ContactPhone { get; set; }
        public string? Description { get; set; }
    }


}
