using Cf.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cf.Domain.Models;
using Cf.WebApi.Routing;
using Cf.Contracts.Responses;

namespace Cf.WebApi.Endpoints;

public static class ServiceInformationEndpoints
{
    private const string Tag = "ServiceInfo";
    private const string GroupName = "serviceInfo";

    public static void MapServiceInfoRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup(GroupName)
            .WithTags(Tag)
        .HasApiVersion(1);

        group.MapPost(AddAsync);
        group.MapPatch(UpdateAsync);
        group.MapGet(GetByServiceIdAsync);
        group.MapGet("Status", GetServiceStatusByIdAsync);
        group.MapPost("UploadPdfBytes", UploadPdfBytesAsync);
    }

    [Authorize(Roles = "Service")]
    private static async Task AddAsync([FromServices] IServicelnfoService additionalInfoService, IHttpContextAccessor httpContextAccessor, [FromBody] ServiceAdditionalInfoModel additionalInfo) =>
        await additionalInfoService.AddAsync(GetServiceId(httpContextAccessor), additionalInfo);

    [Authorize(Roles = "Service")]
    private static async Task UpdateAsync([FromServices] IServicelnfoService additionalInfoService, IHttpContextAccessor httpContextAccessor,[FromBody] ServiceAdditionalInfoModel additionalInfo) =>
        await additionalInfoService.UpdateAsync(GetServiceId(httpContextAccessor), additionalInfo);

    [Authorize(Roles = "Service")]
    private static async Task<Response.ServiceAdditionalFields> GetByServiceIdAsync([FromServices] IServicelnfoService additionalInfoService, IHttpContextAccessor httpContextAccessor) =>
        await additionalInfoService.GetByServiceIdAsync(GetServiceId(httpContextAccessor));

    [Authorize(Roles = "Service")]
    private static async Task<Response.ServiceStatusInfo> GetServiceStatusByIdAsync([FromServices] IServicelnfoService additionalInfoService, IHttpContextAccessor httpContextAccessor) =>
        await additionalInfoService.GetStatusByIdAsync(GetServiceId(httpContextAccessor));

    [Authorize(Roles = "Service")]
    private static async Task UploadPdfBytesAsync([FromServices] IServiceDocumentService pdfService, IHttpContextAccessor httpContextAccessor, [FromBody] string base64Content) =>
        await pdfService.SavePdfAsync(base64Content, GetServiceId(httpContextAccessor));

    private static string? GetServiceId(IHttpContextAccessor httpContextAccessor) =>
        httpContextAccessor.HttpContext?.User.FindFirst("https://CarFans.com/id")?.Value;
}

