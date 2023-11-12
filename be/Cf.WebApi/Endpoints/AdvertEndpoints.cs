using Cf.Application.Interfaces;
using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Models;
using Cf.WebApi.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        group.MapPost(AddAsync).Produces<Response.AdvertIdResponse>();
        group.MapGet("Adverts", GetListAsync).Produces<List<Advert>>();
        group.MapGet("Id", GetByIdAsync).Produces<Response.AdvertResponse>();
        group.MapPut(UpdateAsync);
        group.MapDelete(DeleteAsync);
    }

    [Authorize]
    private static async Task<Response.AdvertIdResponse> AddAsync(IAdvertService service, AdvertModel model)
    {
        var advert = await service.CreateAsync(model);

        return advert;
    }

    [Authorize]
    private static async Task<List<Advert>> GetListAsync(IAdvertService service)
    {
        var adverts = await service.GetListAsync();

        return adverts;
    }

    [Authorize]
    private static async Task<Response.AdvertResponse> GetByIdAsync(IAdvertService service, Guid id)
    {
        var advert = await service.GetByIdAsync(id);

        return advert;
    }

    [Authorize]
    private static async Task UpdateAsync(IAdvertService service, Guid id, [FromBody] AdvertUpdateModel model) =>
        await service.UpdateAsync(id, model);

    [Authorize]
    private static async Task DeleteAsync(IAdvertService service, Guid id) =>
        await service.DeleteAsync(id);
}

