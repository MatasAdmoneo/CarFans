using Cf.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cf.Domain.Aggregates.Services;

public class Service : Entity
{
    public string ServiceId { get; set; }
    public ServiceStatus Status { get; set; }
    public string? ServiceName { get; set; }

    public string? City { get; set; }
    public List<byte[]>? Data { get; set; }
    public string? Adress {  get; set; }
    public List<WorkingDay>? WeeklyWorkingHours { get; set; }
    public string? ContactPhone { get; set; }
    public string? Description { get; set; }

    public Service() { }

    public Service(string serviceId, ServiceStatus status, string serviceName, string city, string adress, List<WorkingDay> weeklyWorkingHours, string contactPhone, string? description)
    {
        ServiceId = serviceId;
        Status = status;
        ServiceName = serviceName;
        City = city;       
        Adress = adress;
        WeeklyWorkingHours = weeklyWorkingHours;
        ContactPhone = contactPhone;
        Description = description;
    }

    public Service(string serviceId, string serviceName, string adress, List<WorkingDay> weeklyWorkingHours, string contactPhone, string description)
    {
        ServiceId = serviceId;
        Status = ServiceStatus.Pending;
        ServiceName = serviceName;
        Adress = adress;
        WeeklyWorkingHours = weeklyWorkingHours;
        ContactPhone = contactPhone;
        Description = description;

    }

    public Service(string serviceId, byte[] data) : base()
    {
        ServiceId = serviceId;
        Status = ServiceStatus.CreatedInDataBase;
        Data = new List<byte[]> { data };
    }

    public void AddData(byte[] pdfBytes)
    {
        Data.Add(pdfBytes);
        UpdatedDate = DateTime.UtcNow;
    }

    [NotMapped]
    public class WorkingDay
    {
        public required DayOfWeek DayOfWeek { get; set; }
        public required string StartTime { get; set; }
        public required string EndTime { get; set; }
        public required string LunchBreakStartTime { get; set; }
        public required string LunchBreakEndTime { get; set; }
    }
}

