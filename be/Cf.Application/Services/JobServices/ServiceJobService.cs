using Cf.Application.Services.Interfaces;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Exceptions;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Cf.Domain.Aggregates.Jobs;
using Cf.Domain.Enums;
using Cf.Contracts.Mappers;

namespace Cf.Application.Services.JobServices;

public class ServiceJobService : IServiceJobService
{

    private readonly Context _context;

    public ServiceJobService(Context context)
    {
        _context = context;
    }

    public async Task<Contracts.Responses.Response.JobIdResponse> CreateAsync(Guid advertId, string? serviceId, JobModel model)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == advertId);

        if (advert == null)
            throw new BadRequestException(DomainErrors.Job.AdvertNotFound);

        if (serviceId == null)
            throw new ApplicationException();

        var job = new Job(serviceId, model.StartDate, model.Price, model.Description);
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

        if (model.Status == JobStatus.Accepted || model.Status == JobStatus.Declined)
            throw new ApplicationException();

        ValidateUpdate(model.Status, job.Status);

        job.UpdateStatus(model.Status);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Job>> GetListAsync(string? serviceId)
    {
        if (serviceId is null)
            throw new ApplicationException();

        var jobs = await _context.Jobs.Where(x => x.ServiceId == serviceId).ToListAsync();

        return jobs;
    }

    private void ValidateUpdate(JobStatus newStatus, JobStatus oldStatus)
    {
        // Pending can only bet set on creation
        if (newStatus == JobStatus.Pending)
            throw new BadRequestException(DomainErrors.Job.IncorrectStatus);

        // Job can only be accepted or declined when it is pending
        if (newStatus == JobStatus.Declined || newStatus == JobStatus.Accepted && oldStatus != JobStatus.Pending)
            throw new BadRequestException(DomainErrors.Job.IncorrectStatus);

        // Job can only be started after it was accepted
        if (newStatus == JobStatus.InProgress && oldStatus != JobStatus.Accepted)
            throw new BadRequestException(DomainErrors.Job.IncorrectStatus);

        // Job can only be done after it was in progess
        if (newStatus == JobStatus.Done && oldStatus != JobStatus.InProgress)
            throw new BadRequestException(DomainErrors.Job.IncorrectStatus);

        // Job can be cancelled by service after it was created or accepted by the client or in progress of it
        if (newStatus == JobStatus.Cancelled && (oldStatus != JobStatus.Accepted || oldStatus != JobStatus.InProgress || oldStatus != JobStatus.Pending))
            throw new BadRequestException(DomainErrors.Job.IncorrectStatus);
    }
}

