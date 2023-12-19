
using Cf.Contracts.Responses;

namespace Cf.Application.Services.Interfaces;


public interface IAdminJobService
{
    Task DeleteAsync(Guid id);

    Task<List<Response.JobIdResponse>> GetListAsync(Guid advertId);
}