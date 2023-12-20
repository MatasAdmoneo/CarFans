using Microsoft.AspNetCore.Authorization;
using Cf.WebApi.Routing;
using Cf.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cf.WebApi.Endpoints;

public static class ServiceAdvertEndpoints
{
    private const string Tag = "ServiceAdverts";
    private const string GroupName = "serviceAdverts";

    public static void MapServiceAdvertRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
        .MapGroup(GroupName)
        .WithTags(Tag)
        .HasApiVersion(1);

        group.MapGet("{Id}", GetByIdAsync).Produces<Contracts.Responses.Response.ServiceAdvertByIdResponse>();
        group.MapGet(GetListAsync).Produces<Contracts.Responses.Response.ServiceAdvertResponse>();
    }

    [Authorize(Roles = "Service")]
    private static async Task<Contracts.Responses.Response.ServiceAdvertByIdResponse> GetByIdAsync([FromServices] IServiceAdvertService service, [FromRoute] Guid id)
    {
        var advert = await service.GetByIdAsync(id);

        return advert;
    }

    [Authorize(Roles = "Service")]
    private static async Task<List<Contracts.Responses.Response.ServiceAdvertResponse>> GetListAsync([FromServices] IServiceAdvertService service)
    {
        var adverts = await service.GetListAsync();

        return adverts;
    }
}

