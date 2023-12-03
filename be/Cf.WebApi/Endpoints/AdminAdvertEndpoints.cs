using Cf.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Cf.WebApi.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Cf.WebApi.Endpoints;

public static class AdminAdvertEndpoints
{
    private const string Tag = "AdminAdverts";
    private const string GroupName = "adminAdverts";

    public static void MapAdminAdvertRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
        .MapGroup(GroupName)
        .WithTags(Tag)
        .HasApiVersion(1);

        group.MapDelete("Id", DeleteAsync);
    }

    [Authorize(Roles = "Admin")]
    private static async Task DeleteAsync([FromServices] IAdminAdvertService service, Guid id)
    {
        await service.DeleteAsync(id);
    }
}

