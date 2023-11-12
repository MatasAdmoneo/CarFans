using Cf.Application.Interfaces;
using Cf.Contracts.Mappers;
using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Jobs;
using Cf.Domain.Enums;
using Cf.Domain.Exceptions;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cf.Application.JobServices;

public class JobService : IJobService
{
    private readonly Context _context;
    public JobService(Context context)
    {
        _context = context;
    }

    public async Task<Response.JobIdResponse> CreateAsync(Guid advertId, JobModel model)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == advertId);

        if (advert == null)
            throw new BadRequestException(DomainErrors.Job.AdvertNotFound);

        var job = new Job(model.StartDate, model.Price);
        job.Advert = advert;
        job.AdvertId = advertId;

        await _context.AddAsync(job);
        await _context.SaveChangesAsync();

        return job.ToModel();
    }

    public async Task UpdateStatusAsync(Guid id, JobUpdateModel model)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);

        if (job == null)
            throw new NotFoundException(DomainErrors.Job.NotFound);

        ValidateUpdate(model.Status, job.Status);

        job.UpdateStatus(model.Status);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Response.JobIdResponse>> GetListAsync(Guid advertId)
    {
        var jobs = await _context.Jobs.Where(x => x.AdvertId == advertId).ToListAsync();

        return jobs.Select(x => x.ToModel()).ToList();
    }

    private void ValidateUpdate(JobStatus newStatus, JobStatus oldStatus)
    {
        // Pending can only bet set on creation
        if(newStatus == JobStatus.Pending)
            throw new BadRequestException(DomainErrors.Job.IncorrectStatus);
        
        // Job can only be accepted or declined when it is pending
        if(newStatus == JobStatus.Declined || newStatus == JobStatus.Accepted && oldStatus != JobStatus.Pending)
            throw new BadRequestException(DomainErrors.Job.IncorrectStatus);
         
        // Job can only be started after it was accepted
        if(newStatus == JobStatus.InProgress && oldStatus != JobStatus.Accepted)
            throw new BadRequestException(DomainErrors.Job.IncorrectStatus);

        // Job can only be done after it was in progess
        if(newStatus == JobStatus.Done && oldStatus != JobStatus.InProgress)
            throw new BadRequestException(DomainErrors.Job.IncorrectStatus);

        // Job can be cancelled by service after it was created or accepted by the client or in progress of it
        if(newStatus == JobStatus.Cancelled && oldStatus != JobStatus.Accepted || oldStatus != JobStatus.InProgress || oldStatus != JobStatus.Pending)
            throw new BadRequestException(DomainErrors.Job.IncorrectStatus);   
    }
}

