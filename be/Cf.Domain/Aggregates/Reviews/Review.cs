using Cf.Domain.Aggregates.Jobs;

namespace Cf.Domain.Aggregates.Reviews;

public class Review : Entity
{
    public Guid JobId { get; set; }

    public double Score { get; set; }

    public string Description { get; set; }

    public virtual Job Job { get; set; }

    public Review() { }

    public Review(Guid jobId, double score, string description) : base()
    {
        JobId = jobId;
        Score = score;
        Description = description;
    }
}

