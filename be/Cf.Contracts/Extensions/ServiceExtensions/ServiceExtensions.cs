using Cf.Domain.Aggregates.Services;
using Cf.Domain.Models;

namespace Cf.Contracts.Extensions.ServiceExtensions;

public static class ServiceExtensions
{
    public static Service UpdateFields(this Service service, ServiceAdditionalInfoModel additionalInfo)
    {
        service.ServiceName = additionalInfo.ServiceName != null ? additionalInfo.ServiceName : service.ServiceName;
        service.Adress = additionalInfo.Address != null ? additionalInfo.Address : service.Adress;
        service.City = additionalInfo.City != null ? additionalInfo.City : service.City;
        service.ContactPhone = additionalInfo.ContactPhone != null ? additionalInfo.ContactPhone : service.ContactPhone;
        service.Description = additionalInfo.Description != null ? additionalInfo.Description : service.Description;
        service.UpdatedDate = DateTime.UtcNow;

        return service;
    }
}

