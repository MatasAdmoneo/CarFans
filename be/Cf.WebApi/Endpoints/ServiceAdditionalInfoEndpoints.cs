using Cf.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cf.Domain.Models;
using Cf.WebApi.Routing;

namespace Cf.WebApi.Endpoints;

public static class ServiceAdditionalInfoEndpoints
{
    private const string Tag = "ServiceInfo";
    private const string GroupName = "serviceInfo";

    public static void MapServiceInfoRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup(GroupName)
            .WithTags(Tag)
        .HasApiVersion(1);

        group.MapPut(UpdateAsync);
    }

    [Authorize(Roles = "Service")]
    private static async Task UpdateAsync([FromServices] IServicelnfoService additionalInfoService, IHttpContextAccessor httpContextAccessor,[FromBody] ServiceAdditionalInfoModel additionalInfo) =>
        await additionalInfoService.UpdateInfoAsync(GetServiceId(httpContextAccessor), additionalInfo);

    private static string? GetServiceId(IHttpContextAccessor httpContextAccessor) =>
        httpContextAccessor.HttpContext?.User.FindFirst("https://CarFans.com/id")?.Value;
}

