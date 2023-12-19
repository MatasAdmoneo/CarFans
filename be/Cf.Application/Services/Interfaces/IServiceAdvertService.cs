using Cf.Contracts.Responses;

namespace Cf.Application.Services.Interfaces;

public interface IServiceAdvertService
{
    Task<Response.ServiceAdvertByIdResponse> GetByIdAsync(Guid id);

    Task<List<Response.ServiceAdvertResponse>> GetListAsync();
}