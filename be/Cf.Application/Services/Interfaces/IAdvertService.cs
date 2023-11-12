using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Models;

namespace Cf.Application.Interfaces;

public interface IAdvertService
{
    Task<Response.AdvertIdResponse> CreateAsync(AdvertModel model);

    Task<List<Advert>> GetListAsync();

    Task UpdateAsync(Guid id, AdvertUpdateModel model);

    Task<Response.AdvertResponse> GetByIdAsync(Guid id);

    Task DeleteAsync(Guid id);
}

