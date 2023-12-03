using Cf.Domain.Aggregates.Services;
using Cf.Domain.Models;

namespace Cf.Application.Services.Interfaces

{
    public interface IServicelnfoService
    {
        Task UpdateInfoAsync(string? serviceId, ServiceAdditionalInfoModel additionalInfo);
    }
}
