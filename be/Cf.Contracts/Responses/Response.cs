using Cf.Domain.Enums;
using Cf.Domain.Models;

namespace Cf.Contracts.Responses;

public static partial class Response
{
    public record AdvertIdResponse(Guid Id, string Title, string Description, DateTime EndDate);

    public record JobIdResponse(Guid Id, JobStatus Status);

    public record AdvertResponse(string Title, string Desciption, DateTime startDate, DateTime updateDate);

    public record UserAdvertResponse(
        Guid Id,
        string Title,
        string Description,
        DateTime EndDate,
        ProblemType ProblemType,
        string Model,
        string Brand,
        int ManufactureYear,
        bool IsOfferAccepted);

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


    public record ServiceJob(Guid Id, string Title, string Brand, string Model, int ManufactureYear, ProblemType ProblemType, JobStatus Status, decimal? Price);

    public record ServiceAdditionalFields(
        string? ServiceName,
        string? Address,
        string? City,
        List<ServiceWorkingHours>? WeeklyWorkingHours,
        string? ContactPhone,
        string? Description);

    public record ServiceStatusInfo(ServiceStatus Status);

    public record UserJobInfo(Guid Id, decimal? Price, DateTime? StartDate, string? Description, JobStatus Status, string ServiceName, string Address, string City, string Phone, string? ServiceDescription);
}

