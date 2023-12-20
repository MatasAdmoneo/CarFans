using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Exceptions;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Cf.Domain.Enums;
using Cf.Application.Services.Interfaces;
using Cf.Contracts.Mappers;
using Cf.Contracts.Responses;

namespace Cf.Application.Services.JobServices;

public class UserJobService : IUserJobService
{
    private readonly Context _context;

    public UserJobService(Context context)
    {
        _context = context;
    }

    public async Task UpdateStatusAsync(Guid id, JobUpdateModel model)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);

        if (job == null)
            throw new NotFoundException(DomainErrors.Job.NotFound);

        if (model.Status != JobStatus.Accepted && model.Status != JobStatus.Declined)
            throw new ApplicationException();

        job.UpdateStatus(model.Status);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Response.UserJobInfo>> GetListAsync(Guid advertId, string? userId)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == advertId);

        if (advert is null)
            throw new BadRequestException(DomainErrors.Advert.NotFound);

        if (advert.UserId != userId)
            throw new ApplicationException();

        var jobs = await _context.Jobs
            .Where(x => x.AdvertId == advertId)
            .Join(
                _context.Services,
                job => job.ServiceId,
                service => service.ServiceId,
                (job, service) => job.ToUserJobInfo(service)
            )
            .ToListAsync();

        return jobs;
    }
}

