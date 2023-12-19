using Cf.Domain.Models;
using Cf.Contracts.Responses;

namespace Cf.Application.Services.Interfaces;

public interface IServiceJobService
{
    Task<Response.JobIdResponse> CreateAsync(Guid advertId, string? serviceId, JobModel model);

    Task UpdateStatusAsync(Guid id, JobUpdateModel model);

    Task<List<Response.ServiceJob>> GetListAsync(string? serviceId);
}

