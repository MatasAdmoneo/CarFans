using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Models;

namespace Cf.Application.Interfaces;

public interface IAdvertService
{
    Task<Response.AdvertId> CreateAsync(AdvertModel model);

    Task<List<Advert>> GetListAsync();
}

