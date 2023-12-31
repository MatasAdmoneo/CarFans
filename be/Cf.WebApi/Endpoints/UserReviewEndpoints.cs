﻿using Cf.Application.Services.Interfaces;
using Cf.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cf.WebApi.Routing;
using Cf.Contracts.Responses;

namespace Cf.WebApi.Endpoints;

public static class UserReviewEndpoints
{
    private const string Tag = "UserReviews";
    private const string GroupName = "userReviews";

    public static void MapUserReviewRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
        .MapGroup(GroupName)
        .WithTags(Tag)
        .HasApiVersion(1);

        group.MapPost("{jobId}", CreateAsync);
        group.MapGet("{serviceId}", GetByServiceId);
    }

    [Authorize(Roles = "User")]
    private static async Task CreateAsync([FromServices] IUserReviewService service, [FromRoute] Guid jobId, ReviewModel model, IHttpContextAccessor httpContextAccessor) =>
        await service.CreateAsync(GetUserId(httpContextAccessor), jobId, model);
    

    [Authorize(Roles = "User")]
    private static async Task<Response.FullReviewInfo> GetByServiceId([FromServices] IUserReviewService service, [FromRoute] string serviceId) =>
        await service.GetByServiceId(serviceId);
    

    private static string? GetUserId(IHttpContextAccessor httpContextAccessor) =>
        httpContextAccessor.HttpContext?.User.FindFirst("https://CarFans.com/id")?.Value;
}
