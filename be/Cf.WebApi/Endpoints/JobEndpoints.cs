using Cf.Application.Interfaces;
using Cf.Domain.Models;
using Cf.WebApi.Routing;
using Microsoft.AspNetCore.Authorization;

namespace Cf.WebApi.Endpoints;

public static class JobEndpoints
{
    private const string Tag = "Jobs";
    private const string GroupName = "jobs";

    public static void MapJobRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
        .MapGroup(GroupName)
        .WithTags(Tag)
        .HasApiVersion(1);

        group.MapPost(AddAsync);
        group.MapGet(GetListAsync);
        group.MapPatch(UpdateAsync);

    }

    [Authorize]
    private static async Task<Contracts.Responses.Response.JobId> AddAsync(IJobService service, Guid advertId, JobModel model)
    {
        var job = await service.CreateAsync(advertId, model);

        return job;
    }


    private static async Task<List<Contracts.Responses.Response.JobId>> GetListAsync(IJobService service, Guid advertId)
    {
        var jobs = await service.GetListAsync(advertId);

        return jobs;
    }

    [Authorize]
    private static async Task UpdateAsync(IJobService service, Guid jobId, JobUpdateModel updateModel) =>
        await service.UpdateStatusAsync(jobId, updateModel);
    

}

