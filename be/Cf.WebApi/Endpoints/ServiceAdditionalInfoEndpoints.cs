using Cf.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cf.Domain.Models;
using Cf.WebApi.Routing;
using Cf.Contracts.Responses;
using Cf.Domain.Enums;

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
        group.MapGet(GetByServiceIdAsync);
        group.MapGet("Status", GetServiceStatusByIdAsync);
    }

    [Authorize(Roles = "Service")]
    private static async Task UpdateAsync([FromServices] IServicelnfoService additionalInfoService, IHttpContextAccessor httpContextAccessor,[FromBody] ServiceAdditionalInfoModel additionalInfo) =>
        await additionalInfoService.UpdateInfoAsync(GetServiceId(httpContextAccessor), additionalInfo);

    [Authorize(Roles = "Service")]
    private static async Task<Response.ServiceAdditionalFields> GetByServiceIdAsync([FromServices] IServicelnfoService additionalInfoService, IHttpContextAccessor httpContextAccessor) =>
        await additionalInfoService.GetByServiceIdAsync(GetServiceId(httpContextAccessor));

    [Authorize(Roles = "Service")]
    private static async Task<ServiceStatus> GetServiceStatusByIdAsync([FromServices] IServicelnfoService additionalInfoService, IHttpContextAccessor httpContextAccessor) =>
        await additionalInfoService.GetServiceStatusByIdAsync(GetServiceId(httpContextAccessor));

    private static string? GetServiceId(IHttpContextAccessor httpContextAccessor) =>
        httpContextAccessor.HttpContext?.User.FindFirst("https://CarFans.com/id")?.Value;
}

