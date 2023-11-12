using Cf.Domain.Models;

namespace Cf.Application.Interfaces;

public interface IJobService
{
    Task<Contracts.Responses.Response.JobIdResponse> CreateAsync(Guid advertId, JobModel model);

    Task UpdateStatusAsync(Guid id, JobUpdateModel model);

    Task<List<Contracts.Responses.Response.JobIdResponse>> GetListAsync(Guid advertId);

}

