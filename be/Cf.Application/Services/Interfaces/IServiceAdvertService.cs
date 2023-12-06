namespace Cf.Application.Services.Interfaces;

public interface IServiceAdvertService
{
    Task<Contracts.Responses.Response.ServiceAdvertByIdResponse> GetByIdAsync(Guid id);

    Task<List<Contracts.Responses.Response.ServiceAdvertResponse>> GetListAsync();
}

