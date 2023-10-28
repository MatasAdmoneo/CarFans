using Cf.Contracts.Responses;
using Cf.Domain.Models;

namespace Cf.Application.Interfaces;

public interface IAdvertService
{
    Task<Response.AdvertModel> CreateAsync(AdvertModel model);
}

