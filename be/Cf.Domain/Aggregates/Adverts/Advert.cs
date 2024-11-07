using Cf.Domain.Aggregates.Jobs;
using Cf.Domain.Enums;
using Cf.Domain.Models;

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

    public Advert() : base()
    {
        UserId = string.Empty;
        Title = string.Empty;
        Description = string.Empty;
        Model = string.Empty;
        Brand = string.Empty;
    }

    public Advert(
        string userId,
        ProblemType problemType,
        AdvertModel model) : base()
    {
        UserId = userId;
        Title = model.Title;
        ProblemType = problemType;
        Description = model.Description ?? string.Empty;
        Brand = model.Brand;
        Model = model.Model;
        ManufactureYear = model.ManufactureYear;
        Photos = model.Photos ?? new List<string>();
        IsQuestionsFormType = model.IsQuestionsFormType;
        IsSoundBad = model.IsSoundBad;
        IsScentBad = model.IsScentBad;
        IsPanelInvalid = model.IsPanelInvalid;
        IsLeakedLiquids = model.IsLeakedLiquids;
        IsUnstableCar = model.IsUnstableCar;
        EndDate = model.EndDate;
    }
}

