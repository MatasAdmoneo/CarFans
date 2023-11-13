
using Cf.Domain.Models;

namespace Cf.Application.Services.Interfaces;

public interface IServiceJobService
{
    Task<Contracts.Responses.Response.JobIdResponse> CreateAsync(Guid advertId, string? serviceId, JobModel model);

    Task UpdateStatusAsync(Guid id, JobUpdateModel model);
}

