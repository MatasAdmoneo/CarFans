using Cf.Application.Interfaces;
using Cf.Contracts.Mappers;
using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Exceptions;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Cf.Application.Services.AdvertServices;

public class AdvertService : IAdvertService
{
    private readonly Context _context;
    private readonly IJobService _jobService;

    public AdvertService(Context context, IJobService service)
    {
        _context = context;
        _jobService = service;
    }

    public async Task<Response.AdvertIdResponse> CreateAsync(AdvertModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Description))
            throw new ApplicationException();

        var advert = new Advert(model.Title, model.Description);

        await _context.AddAsync(advert);
        await _context.SaveChangesAsync();

        return advert.ToAdvertIdModel();
    }

    public async Task<List<Advert>> GetListAsync()
    {
        var adverts = await _context.Adverts.ToListAsync();

        return adverts;
    }

    public async Task UpdateAsync(Guid id, AdvertUpdateModel model)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == id);

        if (advert == null)
            throw new NotFoundException(DomainErrors.Advert.NotFound);

        advert.ToUpdatedAdvert(model);

        await _context.SaveChangesAsync();
    }

    public async Task<Response.AdvertResponse> GetByIdAsync(Guid id)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == id);

        if (advert == null)
            throw new NotFoundException(DomainErrors.Advert.NotFound);

        return advert.ToAdvertModel();
    }

    public async Task DeleteAsync(Guid id)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == id);

        if (advert == null)
            throw new NotFoundException(DomainErrors.Advert.NotFound);

        var jobs = await _jobService.GetListAsync(id);

        foreach (var job in jobs)
        {
            if (job.Status != Domain.Enums.JobStatus.Declined)
                throw new BadRequestException(DomainErrors.Advert.ActiveJobs);
        }

        _context.Remove(advert);
        await _context.SaveChangesAsync();
        
    }
}

