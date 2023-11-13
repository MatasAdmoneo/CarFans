
using Cf.Domain.Models;

namespace Cf.Application.Services.Interfaces;

public interface IUserJobService
{
    Task<List<Contracts.Responses.Response.JobIdResponse>> GetListAsync(Guid advertId);

    Task UpdateStatusAsync(Guid id, JobUpdateModel model);
}

