using Cf.Domain.Aggregates.Services;
using Cf.Domain.Models;

namespace Cf.Application.Services.Interfaces
{
    public interface IServiceWorkingDaysService
    {
        public Task CreateAsync(Guid serviceId, List<ServiceWorkingHours> serviceWorkingHours);
    }
}