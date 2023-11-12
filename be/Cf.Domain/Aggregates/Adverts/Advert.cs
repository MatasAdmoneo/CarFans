using Cf.Domain.Aggregates.Jobs;

namespace Cf.Domain.Aggregates.Adverts;

public class Advert : Entity
{
    public string UserId { get; private set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public List<string> Photos { get; set; } = new List<string>();

    public List<Job> Jobs { get; set; } = new List<Job>();

    public Advert(
        string userId,
        string title,
        string description,
        List<string> photos) : base()
    {
        UserId = userId;
        Title = title;
        Description = description;
        Photos = photos;
    }
}

