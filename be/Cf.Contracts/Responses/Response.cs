using Cf.Domain.Enums;

namespace Cf.Contracts.Responses;

public static partial class Response
{
    public record AdvertIdResponse(Guid Id, string Title, string Description);

    public record JobIdResponse(Guid Id, JobStatus Status);

    public record AdvertResponse(string Title, string Desciption, DateTime startDate, DateTime updateDate);
}

