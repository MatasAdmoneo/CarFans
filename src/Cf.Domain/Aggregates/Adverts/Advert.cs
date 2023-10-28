namespace Cf.Domain.Aggregates.Adverts;

public class Advert
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Advert(string title, string description)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
    }
}

