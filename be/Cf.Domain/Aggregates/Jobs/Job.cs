using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Aggregates.Reviews;
using Cf.Domain.Enums;

namespace Cf.Domain.Aggregates.Jobs;

public class Job : Entity
{
    public string ServiceId {  get; set; }

    public JobStatus Status { get; private set; }

    public decimal? Price { get; private set; }

    public DateTime? StartDate { get; private set; }

    public string? Description { get; set; }

    public Guid AdvertId { get; set; }

    public virtual Advert Advert { get; set; }

    public virtual Review Review { get; set; }

    public Job(string serviceId, DateTime? startDate, decimal? price, string? description) : base()
    {
        ServiceId = serviceId;
        Status = JobStatus.Pending;
        StartDate = startDate;
        Price = price;
        Description = description;
    }

    public void UpdateStatus(JobStatus jobStatus)
    {
        Status = jobStatus;
        UpdateDate();
    }

    public void UpdatePrice(decimal? price)
    {
        Price = price;
        UpdateDate();
    }

    public void UpdateStartDate(DateTime? startDate)
    {
        StartDate = startDate;
        UpdateDate();
    }
}

