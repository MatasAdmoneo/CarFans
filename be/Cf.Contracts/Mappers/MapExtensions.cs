﻿using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Aggregates.Jobs;
using Cf.Domain.Aggregates.Reviews;
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

    public static Response.UserAdvertResponse ToUserAdvertModel(this Advert model, bool isOfferAccepted) =>
        new(model.Id, model.Title, model.Description, model.EndDate, model.ProblemType, model.Model, model.Brand, model.ManufactureYear, isOfferAccepted);

    public static Response.ServiceInfo ToModel(this Service service) =>
        new(service.ServiceId, service.Status, service.CreatedDate);

    public static Response.ServiceAdvertResponse ToServiceAdvertModel(this Advert model) =>
        new(model.Id, model.Title, model.Description, model.Brand, model.Model, model.ManufactureYear, model.ProblemType, model.EndDate);

    public static Response.ServiceAdvertByIdResponse ToServiceAdvertByIdModel(this Advert model) =>
        new(model.Title, model.Description, model.Brand, model.Model, model.ManufactureYear, model.ProblemType, model.Photos, model.IsQuestionsFormType, model.IsSoundBad, model.IsScentBad, model.IsPanelInvalid, model.IsLeakedLiquids, model.IsUnstableCar, model.EndDate);

    public static Response.ReviewInfo ToReviewModel(this Review review) =>
        new(review.FullName, review.Rating, review.Description);

    public static Response.ServiceJob ToServiceJob(this Job model) =>
        new(model.Id, model.Advert.Title, model.Advert.Brand, model.Advert.Model, model.Advert.ManufactureYear, model.Advert.ProblemType, model.Status, model.Price);
    
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
            service.Address,
            service.City,
            convertedHours,
            service.ContactPhone,
            service.Description);
    }

    public static Response.UserJobInfo ToUserJobInfo(this Job job, Service service) =>
        new(job.Id, job.Price, job.StartDate, job.Description, job.Status, service.ServiceName!, service.Address!, service.City!, service.ContactPhone!, service.Description, service.ServiceId);
}

