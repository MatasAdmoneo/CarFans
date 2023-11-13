using Cf.Application.Services.Interfaces;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Exceptions;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cf.Application.Services.AdvertServices;

public class AdminAdvertService : IAdminAdvertService
{
    private readonly Context _context;

    public AdminAdvertService(Context context)
    {
        _context = context;
    }

    public async Task DeleteAsync(Guid id)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == id);

        if (advert == null)
            throw new NotFoundException(DomainErrors.Advert.NotFound);

        var jobs = _context.Jobs.Where(x => x.AdvertId == id);

        foreach (var job in jobs) 
            _context.Remove(job);

        _context.Remove(advert);

        await _context.SaveChangesAsync();
    }
}

