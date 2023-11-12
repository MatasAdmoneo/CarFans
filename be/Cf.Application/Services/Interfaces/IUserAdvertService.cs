using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Models;

namespace Cf.Application.Interfaces;

public interface IUserAdvertService
{
    Task<Response.AdvertIdResponse> CreateAsync(AdvertModel model, string? userId);

    Task<List<Advert>> GetListAsync(string? userId);

    Task UpdateAsync(Guid id, string? userId, AdvertUpdateModel model);

    Task<Response.AdvertResponse> GetByIdAsync(Guid id, string? userId);

    Task DeleteAsync(Guid id, string? userId);
}

