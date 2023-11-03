
namespace Cf.Domain.Aggregates;

public class Entity
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public Entity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }

    public void UpdateDate()
    {
        UpdatedDate = DateTime.UtcNow;
    }
}

