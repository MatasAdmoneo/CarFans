using Cf.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Cf.WebApi.Routing;
using Cf.Application.Services.Interfaces;
using Cf.Domain.Aggregates.Jobs;

namespace Cf.WebApi.Endpoints;

public static class ServiceJobEndpoints
{
    private const string Tag = "ServiceJobs";
    private const string GroupName = "serviceJobs";

    public static void MapServiceJobRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
        .MapGroup(GroupName)
        .WithTags(Tag)
        .HasApiVersion(1);

        group.MapPost(AddAsync);
        group.MapPut(UpdateAsync);
        group.MapGet(GetListAsync);
    }

    [Authorize(Roles = "Service")]
    private static async Task<Contracts.Responses.Response.JobIdResponse> AddAsync(IServiceJobService service, IHttpContextAccessor httpContextAccessor, Guid advertId, JobModel model)
    {
        var job = await service.CreateAsync(advertId, GetServiceId(httpContextAccessor),model);

        return job;
    }

    [Authorize(Roles = "Service")]
    private static async Task UpdateAsync(IServiceJobService service, Guid jobId, JobUpdateModel model)
    {
        await service.UpdateStatusAsync(jobId, model);
    }

    [Authorize(Roles = "Service")]
    private static async Task<List<Job>> GetListAsync(IServiceJobService service, IHttpContextAccessor httpContextAccessor)
    {
        var jobs = await service.GetListAsync(GetServiceId(httpContextAccessor));

        return jobs;
    }

    private static string? GetServiceId(IHttpContextAccessor httpContextAccessor) =>
        httpContextAccessor.HttpContext?.User.FindFirst("https://CarFans.com/id")?.Value;
}

