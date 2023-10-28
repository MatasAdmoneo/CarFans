namespace Cf.WebApi.Endpoints;

public static class AdvertEndpoints
{
    private const string Tag = "Documents";
    private const string GroupName = "documents";

    public static void MapAdvertRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
        .MapGroup(GroupName)
        .WithTags(Tag)
        .HasApiVersion(1);
    }
}

