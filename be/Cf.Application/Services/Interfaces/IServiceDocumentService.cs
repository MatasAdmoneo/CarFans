
namespace Cf.Application.Services.Interfaces;

public interface IServiceDocumentService
{
    Task SavePdfAsync(byte[] pdfBytes, string? serviceId);
}

