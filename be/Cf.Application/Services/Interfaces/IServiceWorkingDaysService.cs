using Cf.Domain.Models;

namespace Cf.Application.Services.Interfaces
{
    public interface IServiceWorkingDaysService
    {
        public void AddByServiceId(Guid serviceId, List<ServiceWorkingHours> serviceWorkingHours);

        public void RemoveByServiceId(Guid serviceId);
    }
}
