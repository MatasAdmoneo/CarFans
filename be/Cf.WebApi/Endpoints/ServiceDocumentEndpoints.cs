using Cf.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cf.WebApi.Endpoints;

public static class ServiceDocumentEndpoints
{
    private const string Tag = "ServiceDocuments";
    private const string GroupName = "serviceDocuments";

    public static void MapServiceDocumentRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup(GroupName)
            .WithTags(Tag)
        .HasApiVersion(1);

        group.MapPost("UploadPdf", UploadPdfAsync);
        group.MapPost("UploadpdfBytes", UploadPdfBytesAsync);
    }

    [Authorize(Roles = "Service")]
    private static async Task UploadPdfAsync([FromServices] IServiceDocumentService pdfService, IHttpContextAccessor httpContextAccessor, [FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ApplicationException();
        }

        // Add your logic to save the PDF file using the pdfService
        // For example, you can save it to a specific folder or a database
        var pdfBytes = await ReadFileBytesAsync(file);

        // Your PDF service method to save the PDF
        await pdfService.SavePdfAsync(pdfBytes, GetServiceId(httpContextAccessor));
    }

    [Authorize(Roles = "Service")]
    private static async Task UploadPdfBytesAsync([FromServices] IServiceDocumentService pdfService, IHttpContextAccessor httpContextAccessor, [FromBody] byte[] pdfBytes)
    {
        if (pdfBytes == null || pdfBytes.Length == 0)
        {
            throw new ApplicationException();
        }

        // Your PDF service method to save the PDF
        await pdfService.SavePdfAsync(pdfBytes, GetServiceId(httpContextAccessor));
    }

    private static async Task<byte[]> ReadFileBytesAsync(IFormFile file)
    {
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    private static string? GetServiceId(IHttpContextAccessor httpContextAccessor) =>
        httpContextAccessor.HttpContext?.User.FindFirst("https://CarFans.com/id")?.Value;
}

