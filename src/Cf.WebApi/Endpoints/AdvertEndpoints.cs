using Cf.Application.Interfaces;
using Cf.Contracts.Responses;
using Cf.Domain.Models;
using Cf.WebApi.Routing;

namespace Cf.WebApi.Endpoints;

public static class AdvertEndpoints
{
    private const string Tag = "Adverts";
    private const string GroupName = "adverts";

    public static void MapAdvertRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
        .MapGroup(GroupName)
        .WithTags(Tag)
        .HasApiVersion(1);

        group.MapPost(Add).Produces<Response.AdvertModel>();
    }

    private static async Task<Response.AdvertModel> Add(IAdvertService service, AdvertModel model)
    {
        var advert = await service.CreateAsync(model);

        return advert;
    }
}

