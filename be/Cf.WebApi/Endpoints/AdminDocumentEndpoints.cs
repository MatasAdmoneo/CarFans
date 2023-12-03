using Cf.Application.Services.Interfaces;
using Cf.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Cf.WebApi.Endpoints;

public static class AdminDocumentEndpoints
{
    private const string Tag = "AdminDocuments";
    private const string GroupName = "adminDocuments";

    public static void MapAdminDocumentRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
        .MapGroup(GroupName)
        .WithTags(Tag)
        .HasApiVersion(1);

        group.MapGet("Services", GetServicesAsync);
        group.MapGet("Id", GetPdfZipAsync);
        group.MapPost("Id", UpdateStatusAsync);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<List<Contracts.Responses.Response.ServiceInfo>> GetServicesAsync([FromServices] IAdminDocumentService service) =>
        await service.GetServicesAsync();


    [Authorize(Roles = "Admin")]
    private static async Task UpdateStatusAsync([FromServices] IAdminDocumentService service, string serviceId, [FromBody] ServiceStatus status) =>
        await service.UpdateStatusAsync(serviceId, status);

    [Authorize(Roles = "Admin")]
    private static async Task GetPdfZipAsync([FromServices] IAdminDocumentService service, string serviceId, HttpContext httpContext)
    {
        var zipBytes = await service.GetPdfsAsync(serviceId);

        var contentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = "pdfs.zip",
            Size = zipBytes.Length
        };

        httpContext.Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
        httpContext.Response.ContentType = "application/zip";

        await httpContext.Response.Body.WriteAsync(zipBytes, 0, zipBytes.Length);
    }
}

