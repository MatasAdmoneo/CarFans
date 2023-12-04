using Cf.Application.Services.Interfaces;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Exceptions;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Cf.Contracts.Mappers;

namespace Cf.Application.Services.AdvertServices;

public class ServiceAdvertService : IServiceAdvertService
{
    private readonly Context _context;

    public ServiceAdvertService(Context context)
    {
        _context = context;
    }

    public async Task<Contracts.Responses.Response.ServiceAdvertByIdResponse> GetByIdAsync(Guid id)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == id);

        if (advert == null)
            throw new NotFoundException(DomainErrors.Advert.NotFound);

        return advert.ToServiceAdvertByIdModel();
    }

    public async Task<List<Contracts.Responses.Response.ServiceAdvertResponse>> GetListAsync()
    {
        var adverts = await _context.Adverts.Where(a => a.EndDate > DateTime.UtcNow).OrderByDescending(x => x.EndDate).ToListAsync();
        List<Contracts.Responses.Response.ServiceAdvertResponse> result = new();
        foreach (var advert in adverts)
        {
            result.Add(advert.ToServiceAdvertModel());
        }

        return result;
    }
}

