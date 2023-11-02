using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Enums;

namespace Cf.Domain.Aggregates.Jobs;

public class Job : Entity
{
    public JobStatus Status { get; private set; }

    public decimal? Price { get; private set; }

    public DateTime? StartDate { get; private set; }

    public Guid AdvertId { get; set; }
    public Advert Advert { get; set; }

    public Job(DateTime? startDate, decimal? price) : base()
    {
        Status = JobStatus.Pending;
        StartDate = startDate;
        Price = price;
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

