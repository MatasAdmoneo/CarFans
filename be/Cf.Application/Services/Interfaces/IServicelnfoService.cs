using Cf.Contracts.Responses;
using Cf.Domain.Enums;
using Cf.Domain.Models;

namespace Cf.Application.Services.Interfaces

{
    public interface IServicelnfoService
    {
        Task AddAsync(string? serviceId, ServiceAdditionalInfoModel additionalInfo);

        Task UpdateAsync(string? serviceId, ServiceAdditionalInfoModel additionalInfo);
        
        Task<Response.ServiceAdditionalFields> GetByServiceIdAsync(string? serviceId);

        Task<ServiceStatus> GetStatusByIdAsync(string? serviceId);
    }
}
