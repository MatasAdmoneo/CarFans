
namespace Cf.Application.Services.Interfaces;

public interface IAdminJobService
{
    Task DeleteAsync(Guid id);

    Task<List<Contracts.Responses.Response.JobIdResponse>> GetListAsync(Guid advertId);
}

