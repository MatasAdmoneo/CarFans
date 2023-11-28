
namespace Cf.Application.Services.Interfaces;

public interface IServiceDocumentService
{
    Task SavePdfAsync(string? base64Content, string? serviceId);
}

