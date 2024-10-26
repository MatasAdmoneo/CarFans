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
        var job = await _context.Jobs.Include(x => x.Advert).FirstOrDefaultAsync(x => x.Id == jobId) ?? throw new BadRequestException(DomainErrors.Job.NotFound);
        
        if (userId == null || userId != job.Advert.UserId)
            throw new BadRequestException(DomainErrors.Review.NotValidUser);

        ValidateReview(job, model);

        var review = new Review(model.FullName, model.Rating, model.Description, job.Id);

        await _context.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task<Response.FullReviewInfo> GetByServiceId(string serviceId)
    {
        var reviews = await _context.Reviews.Where(r => r.Job.ServiceId == serviceId).ToListAsync();

        var reviewModels = reviews.Select(x => x.ToReviewModel());
        var averageRating = reviews.Select(x => x.Rating).Average();

        return new(reviewModels, averageRating);
    }

    private static void ValidateReview(Job job, ReviewModel model)
    {
        if (job.Status != JobStatus.Done)
            throw new BadRequestException(DomainErrors.Review.NotFinished);

        if (model.Rating < 0 || model.Rating > 5)
            throw new BadRequestException(DomainErrors.Review.RatingOutOfBounds);
    }
}

