using Cf.Application.Interfaces;
using Cf.Contracts.Mappers;
using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cf.Application.AdvertServices;

public class AdvertService : IAdvertService
{
    private readonly Context _context;

    public AdvertService(Context context)
    {
        _context = context;
    }

    public async Task<Response.AdvertId> CreateAsync(AdvertModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Description))
            throw new ApplicationException();

        var advert = new Advert(model.Title, model.Description);

        await _context.AddAsync(advert);
        await _context.SaveChangesAsync();

        return advert.ToModel();
    }

    public async Task<List<Advert>> GetListAsync()
    {
        var adverts = await _context.Adverts.ToListAsync();

        return adverts;
    }
}

