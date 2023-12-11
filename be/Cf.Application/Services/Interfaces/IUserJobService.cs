
using Cf.Domain.Aggregates.Jobs;
using Cf.Domain.Models;

namespace Cf.Application.Services.Interfaces;

public interface IUserJobService
{
    Task UpdateStatusAsync(Guid id, JobUpdateModel model);

    Task<List<Job>> GetListAsync(Guid advertId, string? userId);
}