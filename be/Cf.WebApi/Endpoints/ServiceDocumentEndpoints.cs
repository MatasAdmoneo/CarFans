﻿using Cf.Application.Services.Interfaces;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Exceptions;
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

        group.MapPost("UploadpdfBytes", UploadPdfBytesAsync);
    }

    [Authorize(Roles = "Service")]
    private static async Task UploadPdfBytesAsync([FromServices] IServiceDocumentService pdfService, IHttpContextAccessor httpContextAccessor, [FromBody] string base64Content)
    {
        if (string.IsNullOrEmpty(base64Content))
        {
            throw new BadRequestException(DomainErrors.Service.NotFound);
        }

        try
        {
            byte[] pdfBytes = Convert.FromBase64String(base64Content);
            await pdfService.SavePdfAsync(pdfBytes, GetServiceId(httpContextAccessor));
        }
        catch (Exception)
        {
            throw new InternalException(DomainErrors.Service.FailedUpload);
        }
    }

    private static string? GetServiceId(IHttpContextAccessor httpContextAccessor) =>
        httpContextAccessor.HttpContext?.User.FindFirst("https://CarFans.com/id")?.Value;
}

