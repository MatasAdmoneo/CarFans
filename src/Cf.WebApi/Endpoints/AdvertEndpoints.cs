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

        group.MapPost(AddAsync).Produces<Response.Advert>();
    }

    private static async Task<Response.Advert> Add() => new Response.Advert(Guid.NewGuid());

    private static async Task<Response.Advert> AddAsync(IAdvertService service, AdvertModel model)
    {
        var advert = await service.CreateAsync(model);

        return advert;
    }
}

