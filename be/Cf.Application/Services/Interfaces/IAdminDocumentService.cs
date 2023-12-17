using Cf.Domain.Enums;
using Cf.Contracts.Responses;

namespace Cf.Application.Services.Interfaces;

public interface IAdminDocumentService
{
    Task<byte[]> GetPdfsAsync(string serviceId);

    Task<List<Response.ServiceInfo>> GetServicesAsync();

    Task UpdateStatusAsync(string serviceId, ServiceStatus status);
}