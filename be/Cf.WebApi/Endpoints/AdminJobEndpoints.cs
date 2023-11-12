using Cf.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cf.WebApi.Endpoints;

public static class AdminJobEndpoints
{
    private const string Tag = "AdminJobs";
    private const string GroupName = "adminJobs";

    public static void MapAdminJobRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
        .MapGroup(GroupName)
        .WithTags(Tag)
        .HasApiVersion(1);

        group.MapDelete("Id", DeleteAsync);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<List<Contracts.Responses.Response.JobIdResponse>> GetAsync([FromServices] IAdminJobService service, Guid advertId)
    {
        var jobs = await service.GetListAsync(advertId);

        return jobs;
    }

    [Authorize(Roles = "Admin")]
    private static async Task DeleteAsync([FromServices] IAdminJobService service, Guid id)
    {
        await service.DeleteAsync(id);
    }
}

