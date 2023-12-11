#pragma warning disable CS8618
using Cf.Domain.Enums;

namespace Cf.Domain.Aggregates.Services;

public class Service : Entity
{
    public string ServiceId { get; set; }
    public ServiceStatus Status { get; set; }
    public string? ServiceName { get; set; }

    public string? City { get; set; }
    public List<byte[]>? Data { get; set; }
    public string? Address {  get; set; }
    public List<WorkingDay>? WeeklyWorkingHours { get; set; }
    public string? ContactPhone { get; set; }
    public string? Description { get; set; }

    public Service() { }

    public Service(string serviceId, ServiceStatus status, string serviceName, string city, string address, List<WorkingDay> weeklyWorkingHours, string contactPhone, string? description)
    {
        ServiceId = serviceId;
        Status = status;
        ServiceName = serviceName;
        City = city;
        Data = new List<byte[]>();
        Address = address;
        WeeklyWorkingHours = weeklyWorkingHours;
        ContactPhone = contactPhone;
        Description = description;
    }

    public void AddData(byte[] pdfBytes)
    {
        Data!.Add(pdfBytes);
        Status = ServiceStatus.Pending;
        UpdatedDate = DateTime.UtcNow;
    }   
}

