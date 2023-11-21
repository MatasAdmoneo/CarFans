using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Aggregates.Jobs;
using Cf.Domain.Aggregates.Services;
using Cf.Domain.Models;
using System.Runtime.CompilerServices;

namespace Cf.Contracts.Mappers;

public static class MapExtensions
{
    public static Response.AdvertIdResponse ToAdvertIdModel(this Advert model) =>
        new(model.Id, model.Title, model.Description);

    public static Response.AdvertResponse ToAdvertModel(this Advert model) =>
        new(model.Title, model.Description, model.CreatedDate, model.UpdatedDate);

    public static Response.JobIdResponse ToModel(this Job model) =>
        new(model.Id, model.Status);

    public static Advert ToUpdatedAdvert(this Advert advert, AdvertUpdateModel updateModel)
    {
        advert.Title = updateModel.Title == null ? advert.Title : updateModel.Title;
        advert.Description = updateModel.Description == null? advert.Description : updateModel.Description;
        advert.UpdateDate();

        return advert;
    }

    public static Response.ServiceInfo ToModel(this Service service) =>
        new(service.ServiceId, service.Status, service.CreatedDate);
}

