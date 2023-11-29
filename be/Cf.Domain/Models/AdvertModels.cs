namespace Cf.Domain.Models;

public class AdvertModel
{
    public string Title { get; set; } = null!;

    public string ProblemType { get; set; } = null!;

    public string? Description { get; set; }

    public string Model { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public int ManufactureYear { get; set; }

    public List<string>? Photos { get; set; }

    public bool IsQuestionsFormType { get; set; } = false;

    public bool IsSoundBad { get; set; } = false;

    public bool IsScentBad { get; set; } = false;

    public bool IsPanelInvalid { get; set; } = false;

    public bool IsLeakedLiquids { get; set; } = false;

    public bool IsUnstableCar { get; set; } = false;

    public DateTime EndDate { get; set; }
}

public class AdvertUpdateModel
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public List<string>? Photos { get; set; }
}
