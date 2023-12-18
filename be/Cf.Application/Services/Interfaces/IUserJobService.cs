using Cf.Contracts.Responses;
using Cf.Domain.Models;

namespace Cf.Application.Services.Interfaces;

public interface IUserJobService
{
    Task UpdateStatusAsync(Guid id, JobUpdateModel model);

    Task<List<Response.UserJobInfo>> GetListAsync(Guid advertId, string? userId);
}