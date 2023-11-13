using Cf.Application.Services.Interfaces;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Exceptions;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Cf.Contracts.Mappers;
using Cf.Domain.Aggregates.Adverts;

namespace Cf.Application.Services.AdvertServices;

public class ServiceAdvertService : IServiceAdvertService
{
    private readonly Context _context;

    public ServiceAdvertService(Context context)
    {
        _context = context;
    }

    public async Task<Contracts.Responses.Response.AdvertResponse> GetByIdAsync(Guid id)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == id);

        if (advert == null)
            throw new NotFoundException(DomainErrors.Advert.NotFound);

        return advert.ToAdvertModel();
    }

    public async Task<List<Advert>> GetListAsync()
    {
        var adverts = await _context.Adverts.ToListAsync();

        return adverts;
    }
}

