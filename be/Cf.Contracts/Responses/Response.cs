﻿using Cf.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Cf.Contracts.Responses;

public static partial class Response
{
    public record AdvertIdResponse(Guid Id, string Title, string Description, DateTime EndDate);

    public record JobIdResponse(Guid Id, JobStatus Status);

    public record AdvertResponse(string Title, string Desciption, DateTime startDate, DateTime updateDate);

    public record ServiceAdvertResponse(
        Guid Id,
        string Title,
        string Description,
        string Brand,
        string Model,
        int ManufactureYear,
        ProblemType ProblemType,
        DateTime EndDate);

    public record ServiceAdvertByIdResponse(
        string Title,
        string Description,
        string Brand,
        string Model,
        int ManufactureYear,
        ProblemType ProblemType,
        List<string> Photos,
        bool IsQuestionsFormType,
        bool IsSoundBad,
        bool IsScentBad,
        bool IsPanelInvalid,
        bool IsLeakedLiquids,
        bool IsUnstableCar,
        DateTime EndDate);

    public record ServiceInfo(string Id, ServiceStatus Status, DateTime CreatedDate);

    public record ReviewInfo(string FullName, double Rating, string Description);

    public record FullReviewInfo(IEnumerable<ReviewInfo> Reviews, double AverageRating);

    public record UserJobInfo(Guid id, decimal? Price, DateTime? StartDate, string? Description, JobStatus Status, string ServiceId, Guid AdvertId);
}

