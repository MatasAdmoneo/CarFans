using Microsoft.AspNetCore.Authorization;
using Cf.WebApi.Routing;
using Cf.Domain.Aggregates.Adverts;
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

        group.MapGet("Id", GetByIdAsync).Produces<Contracts.Responses.Response.AdvertResponse>();
        group.MapGet(GetListAsync);
    }

    [Authorize(Roles = "Service")]
    private static async Task<Contracts.Responses.Response.AdvertResponse> GetByIdAsync([FromServices] IServiceAdvertService service, Guid id)
    {
        var advert = await service.GetByIdAsync(id);

        return advert;
    }

    [Authorize(Roles = "Service")]
    private static async Task<List<Advert>> GetListAsync([FromServices] IServiceAdvertService service)
    {
        var adverts = await service.GetListAsync();

        return adverts;
    }
}

