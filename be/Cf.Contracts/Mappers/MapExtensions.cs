using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Aggregates.Jobs;
using Cf.Domain.Aggregates.Services;
using Cf.Domain.Models;

namespace Cf.Contracts.Mappers;

public static class MapExtensions
{
    public static Response.AdvertIdResponse ToAdvertIdModel(this Advert model) =>
        new(model.Id, model.Title, model.Description, model.EndDate);

    public static Response.AdvertResponse ToAdvertModel(this Advert model) =>
        new(model.Title, model.Description, model.CreatedDate, model.UpdatedDate);

    public static Response.JobIdResponse ToModel(this Job model) =>
        new(model.Id, model.Status);

    public static Advert ToUpdatedAdvert(this Advert advert, AdvertUpdateModel updateModel)
    {
        advert.Title = updateModel.Title == null ? advert.Title : updateModel.Title;
        advert.Description = updateModel.Description == null ? advert.Description : updateModel.Description;
        advert.UpdateDate();

        return advert;
    }

    public static Response.ServiceInfo ToModel(this Service service) =>
        new(service.ServiceId, service.Status, service.CreatedDate);

    public static Response.ServiceAdvertResponse ToServiceAdvertModel(this Advert model) =>
        new(model.Id, model.Title, model.Description, model.Brand, model.Model, model.ManufactureYear, model.ProblemType, model.EndDate);

    public static Response.ServiceAdvertByIdResponse ToServiceAdvertByIdModel(this Advert model) =>
        new(model.Title, model.Description, model.Brand, model.Model, model.ManufactureYear, model.ProblemType, model.Photos, model.IsQuestionsFormType, model.IsSoundBad, model.IsScentBad, model.IsPanelInvalid, model.IsLeakedLiquids, model.IsUnstableCar, model.EndDate);

    public static Response.ServiceAdditionalFields ToServiceInfoModel(this Service service)
    {
        var convertedHours = service.WeeklyWorkingHours?
            .Select(x => new ServiceWorkingHours
            {
                DayOfWeek = (int)x.DayOfWeek,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                LunchBreakStartTime = x.LunchBreakStartTime,
                LunchBreakEndTime = x.LunchBreakEndTime
            })
            .ToList() ?? new List<ServiceWorkingHours>();

        return new Response.ServiceAdditionalFields(
            service.ServiceName,
            service.Adress,
            service.City,
            convertedHours,
            service.ContactPhone,
            service.Description);
    }
}

