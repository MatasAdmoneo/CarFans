using Cf.Domain.Enums;

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
}

