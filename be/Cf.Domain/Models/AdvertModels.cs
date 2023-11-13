namespace Cf.Domain.Models;

public class AdvertModel
{
    public string? Title { get; set; }  

    public string? Description { get; set; }

    public List<string>? Photos { get; set; }
}

public class AdvertUpdateModel
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public List<string>? Photos { get; set; }
}
