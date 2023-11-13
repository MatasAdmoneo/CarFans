using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Exceptions;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Cf.Domain.Enums;
using Cf.Contracts.Mappers;
using Cf.Application.Services.Interfaces;

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

        if (model.Status != JobStatus.Accepted || model.Status != JobStatus.Declined)
            throw new ApplicationException();

        ValidateUpdate(model.Status, job.Status);

        job.UpdateStatus(model.Status);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Contracts.Responses.Response.JobIdResponse>> GetListAsync(Guid advertId)
    {
        var jobs = await _context.Jobs.Where(x => x.AdvertId == advertId).ToListAsync();

        return jobs.Select(x => x.ToModel()).ToList();
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
        if (newStatus == JobStatus.Cancelled && oldStatus != JobStatus.Accepted || oldStatus != JobStatus.InProgress || oldStatus != JobStatus.Pending)
            throw new BadRequestException(DomainErrors.Job.IncorrectStatus);
    }
}

