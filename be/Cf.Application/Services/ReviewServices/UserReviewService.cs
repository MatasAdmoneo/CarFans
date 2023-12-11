using Cf.Application.Services.Interfaces;
using Cf.Domain.Aggregates.Jobs;
using Cf.Domain.Exceptions;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Cf.Domain.Enums;
using Cf.Domain.Aggregates.Reviews;
using Cf.Contracts.Mappers;
using Cf.Contracts.Responses;

namespace Cf.Application.Services.ReviewServices;

public class UserReviewService : IUserReviewService
{
    private readonly Context _context;

    public UserReviewService(Context context)
    {
        _context = context;
    }

    public async Task CreateAsync(string? userId, Guid jobId, ReviewModel model)
    {
        var job = await _context.Jobs.Include(x => x.Advert).FirstOrDefaultAsync(x => x.Id == jobId);

        ValidateReview(userId, job);

        var review = new Review(job.Id, model.Score, model.Description);

        await _context.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Response.ReviewInfo>> GetByServiceId(string serviceId)
    {
        var reviews = await _context.Reviews.Include(x => x.Job).Where(r => r.Job.ServiceId == serviceId).ToListAsync();

        return reviews.Select(x => x.ToReviewModel()).ToList();
    }

    private void ValidateReview(string? userId, Job? job)
    {
        if (job == null)
            throw new BadRequestException(DomainErrors.Job.NotFound);

        if (userId == null)
            throw new ApplicationException();

        if (userId != job.Advert.UserId)
            throw new BadRequestException(DomainErrors.Review.NotValidUser);

        if (job.Status != JobStatus.Done)
            throw new BadRequestException(DomainErrors.Review.NotFinished);
    }
}

