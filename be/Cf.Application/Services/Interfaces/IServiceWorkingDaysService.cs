using Cf.Domain.Aggregates.Services;
using Cf.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cf.Application.Services.Interfaces
{
    public interface IServiceWorkingDaysService
    {
        public void AddWorkingDaysByServiceId(Guid serviceId, List<ServiceWorkingHours> serviceWorkingHours);

        public void RemoveWorkingDaysByServiceId(Guid serviceId);
    }
}
