using Cf.Domain.Models;

namespace Cf.Application.Interfaces;

public interface IJobService
{
    Task<Contracts.Responses.Response.JobId> CreateAsync(Guid advertId, JobModel model);

    Task UpdateStatusAsync(Guid id, JobUpdateModel model);

    Task<List<Contracts.Responses.Response.JobId>> GetListAsync(Guid advertId);

}

