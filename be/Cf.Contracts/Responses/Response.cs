using Cf.Domain.Enums;
using Cf.Domain.Models;

namespace Cf.Contracts.Responses;

public static partial class Response
{
    public record AdvertIdResponse(Guid Id, string Title, string Description);

    public record JobIdResponse(Guid Id, JobStatus Status);

    public record AdvertResponse(string Title, string Desciption, DateTime startDate, DateTime updateDate);

    public record ServiceInfo(string Id, ServiceStatus Status, DateTime CreatedDate);
    public record ServiceAdditionalFields(string Id, ServiceAdditionalInfoModel ServiceAdditionalInfo);
}

