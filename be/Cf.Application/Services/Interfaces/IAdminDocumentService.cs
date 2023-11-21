
using Cf.Domain.Enums;

namespace Cf.Application.Services.Interfaces;

public interface IAdminDocumentService
{
    Task<byte[]> GetPdfsAsync(string serviceId);

    Task<List<Contracts.Responses.Response.ServiceInfo>> GetServicesAsync();

    Task UpdateStatusAsync(string serviceId, ServiceStatus status);
}

