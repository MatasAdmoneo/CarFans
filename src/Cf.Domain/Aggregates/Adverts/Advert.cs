using Cf.Domain.Aggregates.Jobs;

namespace Cf.Domain.Aggregates.Adverts;

public class Advert : Entity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public List<Job> Jobs { get; set; } = new List<Job>();

    public Advert(string title, string description) : base()
    {
        Title = title;
        Description = description;
    }
}

