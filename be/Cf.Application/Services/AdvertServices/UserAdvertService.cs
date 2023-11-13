using Cf.Application.Interfaces;
using Cf.Application.Services.Interfaces;
using Cf.Contracts.Mappers;
using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Exceptions;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cf.Application.Services.AdvertServices;

public class UserAdvertService : IUserAdvertService
{
    private readonly Context _context;
    private readonly IUserJobService _jobService;

    public UserAdvertService(Context context, IUserJobService jobService)
    {
        _context = context;
        _jobService = jobService;
    }

    public async Task<Response.AdvertIdResponse> CreateAsync(AdvertModel model, string? userId)
    {
        if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Description) ||string.IsNullOrWhiteSpace(userId))
            throw new ApplicationException();

        if(model.Photos == null || !model.Photos.Any())
            throw new ApplicationException();

        var advert = new Advert(userId, model.Title, model.Description, model.Photos);

        await _context.AddAsync(advert);
        await _context.SaveChangesAsync();

        return advert.ToAdvertIdModel();
    }

    public async Task<List<Advert>> GetListAsync(string? id)
    {
        if(string.IsNullOrWhiteSpace(id))
            throw new ApplicationException();

        var adverts = await _context.Adverts.Where(x => x.UserId == id).ToListAsync();

        return adverts;
    }

    public async Task UpdateAsync(Guid id, string? userId, AdvertUpdateModel model)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == id);

        if (advert == null)
            throw new NotFoundException(DomainErrors.Advert.NotFound);

        if (advert.UserId != userId)
            throw new ApplicationException();

        advert.ToUpdatedAdvert(model);

        await _context.SaveChangesAsync();
    }

    public async Task<Response.AdvertResponse> GetByIdAsync(Guid id, string? userId)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == id);

        if (advert == null)
            throw new NotFoundException(DomainErrors.Advert.NotFound);

        if (advert.UserId != userId)
            throw new ApplicationException();

        return advert.ToAdvertModel();
    }

    public async Task DeleteAsync(Guid id, string? userId)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == id);

        if (advert == null)
            throw new NotFoundException(DomainErrors.Advert.NotFound);

        if(advert.UserId != userId)
            throw new ApplicationException();

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

