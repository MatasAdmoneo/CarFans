﻿using Cf.Domain.Aggregates.Jobs;
using Cf.Domain.Enums;

namespace Cf.Domain.Aggregates.Adverts;

public class Advert : Entity
{
    public string UserId { get; private set; }

    public string Title { get; set; }

    public ProblemType ProblemType { get; set; }

    public string Description { get; set; }

    public string Model { get; set; }

    public string Brand { get; set; }

    public int ManufactureYear { get; set; }

    public List<string> Photos { get; set; } = new List<string>();

    public DateTime EndDate { get; set; }

    public List<Job> Jobs { get; set; } = new List<Job>();

    public bool IsQuestionsFormType { get; set; } = false;

    public bool IsSoundBad { get; set; } = false;

    public bool IsScentBad { get; set; } = false;

    public bool IsPanelInvalid { get; set; } = false;

    public bool IsLeakedLiquids { get; set; } = false;

    public bool IsUnstableCar { get; set; } = false;

    public Advert(
        string userId,
        string title,
        ProblemType problemType,
        string description,
        string brand,
        string model,
        int manufactureYear,
        List<string> photos,
        bool isQuestionsFormType,
        bool isSoundBad,
        bool isScentBad,
        bool isPanelInvalid,
        bool isLeakedLiquids,
        bool isUnstableCar,
        DateTime endDate) : base()
    {
        UserId = userId;
        Title = title;
        ProblemType = problemType;
        Description = description;
        Brand = brand;
        Model = model;
        ManufactureYear = manufactureYear;
        Photos = photos;
        IsQuestionsFormType = isQuestionsFormType;
        IsSoundBad = isSoundBad;
        IsScentBad = isScentBad;
        IsPanelInvalid = isPanelInvalid;
        IsLeakedLiquids = isLeakedLiquids;
        IsUnstableCar = isUnstableCar;
        EndDate = endDate;
    }
}

