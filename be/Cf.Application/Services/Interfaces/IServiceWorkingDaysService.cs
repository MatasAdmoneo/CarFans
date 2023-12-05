using Cf.Domain.Aggregates.Services;
using Cf.Domain.Models;

namespace Cf.Application.Services.Interfaces
{
    public interface IServiceWorkingDaysService
    {
        public Task<List<WorkingDay>> AddByServiceId(Guid serviceId, List<ServiceWorkingHours> serviceWorkingHours);

        public void RemoveByServiceId(Guid serviceId);
    }
}
