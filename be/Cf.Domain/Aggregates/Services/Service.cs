using Cf.Domain.Enums;

namespace Cf.Domain.Aggregates.Services;

public class Service : Entity
{
    public string ServiceId { get; set; }

    public ServiceStatus Status { get; set; }

    public List<byte[]> Data { get; set; }

    public Service() { }

    public Service(string serviceId, byte[] data) : base()
    {
        ServiceId = serviceId;
        Status = ServiceStatus.Pending;
        Data = new List<byte[]> { data };
    }
}

