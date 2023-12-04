using Cf.Application.Interfaces;
using Cf.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Cf.Contracts.Responses;
using Cf.WebApi.Routing;
using Cf.Domain.Aggregates.Adverts;
using Microsoft.AspNetCore.Mvc;

namespace Cf.WebApi.Endpoints;

public static class UserAdvertEndpoints
{
    private const string Tag = "UserAdverts";
    private const string GroupName = "userAdverts";

    public static void MapUserAdvertRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
        .MapGroup(GroupName)
        .WithTags(Tag)
        .HasApiVersion(1);

        group.MapPost(AddAsync).Produces<Response.AdvertIdResponse>();
        group.MapGet(GetListAsync).Produces<List<Advert>>();
        group.MapGet("Id", GetByIdAsync);
        group.MapPut(UpdateAsync);
        group.MapDelete(DeleteAsync);
    }

    [Authorize(Roles = "User")]
    private static async Task<Response.AdvertIdResponse> AddAsync(IUserAdvertService service, IHttpContextAccessor httpContextAccessor, [FromBody] AdvertModel model)
    {
        var advert = await service.CreateAsync(model, GetUserId(httpContextAccessor));

        return advert;
    }

    [Authorize(Roles = "User")]
    private static async Task<List<Advert>> GetListAsync(IUserAdvertService service, IHttpContextAccessor httpContextAccessor)
    {
        var adverts = await service.GetListAsync(GetUserId(httpContextAccessor));

        return adverts;
    }

    [Authorize(Roles = "User")]
    private static async Task<Response.AdvertResponse> GetByIdAsync(IUserAdvertService service, IHttpContextAccessor httpContextAccessor, Guid id)
    {
        var advert = await service.GetByIdAsync(id, GetUserId(httpContextAccessor));

        return advert;
    }

    [Authorize(Roles = "User")]
    private static async Task UpdateAsync(IUserAdvertService service, IHttpContextAccessor httpContextAccessor, Guid id, [FromBody] AdvertUpdateModel model)
    {
        await service.UpdateAsync(id, GetUserId(httpContextAccessor), model);
    }

    [Authorize(Roles = "User")]
    private static async Task DeleteAsync(IUserAdvertService service, IHttpContextAccessor httpContextAccessor, Guid id)
    {
        await service.DeleteAsync(id, GetUserId(httpContextAccessor));
    }

    private static string? GetUserId(IHttpContextAccessor httpContextAccessor) =>
        httpContextAccessor.HttpContext?.User.FindFirst("https://CarFans.com/id")?.Value;
}


