using Cf.Application.Services.Interfaces;
using Cf.Contracts.Responses;
using Cf.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cf.WebApi.Routing;

namespace Cf.WebApi.Endpoints;

public static class UserJobEndpoints
{
    private const string Tag = "UserJobs";
    private const string GroupName = "userJobs";

    public static void MapUserJobRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
        .MapGroup(GroupName)
        .WithTags(Tag)
        .HasApiVersion(1);

        group.MapPut("{jobId}", UpdateAsync);
        group.MapGet("{advertId}", GetListAsync);
    }

    [Authorize(Roles = "User")]
    private static async Task UpdateAsync(IUserJobService service, [FromBody] JobUpdateModel model, [FromRoute] Guid jobId)
    {
        await service.UpdateStatusAsync(jobId, model);
    }

    [Authorize(Roles = "User")]
    private static async Task<List<Response.UserJobInfo>> GetListAsync(IUserJobService service, [FromRoute] Guid advertId, IHttpContextAccessor httpContextAccessor)
    {
        return await service.GetListAsync(advertId, GetUserId(httpContextAccessor));
    }

    private static string? GetUserId(IHttpContextAccessor httpContextAccessor) =>
        httpContextAccessor.HttpContext?.User.FindFirst("https://CarFans.com/id")?.Value;
}

