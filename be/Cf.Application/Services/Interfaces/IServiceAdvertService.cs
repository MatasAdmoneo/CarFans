
using Cf.Domain.Aggregates.Adverts;

namespace Cf.Application.Services.Interfaces;

public interface IServiceAdvertService
{
    Task<Contracts.Responses.Response.AdvertResponse> GetByIdAsync(Guid id);

    Task<List<Advert>> GetListAsync();
}

