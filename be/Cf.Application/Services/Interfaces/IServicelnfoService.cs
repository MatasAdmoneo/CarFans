using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Services;
using Cf.Domain.Enums;
using Cf.Domain.Models;

namespace Cf.Application.Services.Interfaces

{
    public interface IServicelnfoService
    {
        Task UpdateInfoAsync(string? serviceId, ServiceAdditionalInfoModel additionalInfo);
        
        Task<Response.ServiceAdditionalFields> GetByServiceIdAsync(string? serviceId);

        Task<ServiceStatus> GetServiceStatusByIdAsync(string? serviceId);
    }
}
