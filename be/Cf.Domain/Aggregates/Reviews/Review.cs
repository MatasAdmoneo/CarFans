﻿using Cf.Domain.Aggregates.Jobs;

namespace Cf.Domain.Aggregates.Reviews;

public class Review : Entity
{
    public string FullName { get; set; }

    public double Rating { get; set; }

    public string Description { get; set; }

    public Guid JobId { get; set; }

    public virtual Job Job { get; set; }

    public Review() { }

    public Review(string fullName, double rating, string description, Guid jobId) : base()
    {
        FullName = fullName;
        Rating = rating;
        Description = description;
        JobId = jobId;
    }
}

