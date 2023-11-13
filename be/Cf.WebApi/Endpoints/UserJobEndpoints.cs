﻿using Cf.Application.Services.Interfaces;
using Cf.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        group.MapPut("Id", UpdateAsync);
    }

    [Authorize(Roles = "User")]
    private static async Task UpdateAsync(IUserJobService service, [FromBody] JobUpdateModel model, Guid id)
    {
        await service.UpdateStatusAsync(id, model);
    }
}

