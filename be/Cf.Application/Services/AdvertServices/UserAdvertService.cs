using Cf.Application.Interfaces;
using Cf.Application.Services.Interfaces;
using Cf.Contracts.Mappers;
using Cf.Contracts.Responses;
using Cf.Domain.Enums;
using Cf.Domain.Exceptions;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cf.Application.Services.AdvertServices;

public class UserAdvertService : IUserAdvertService
{
    private readonly Context _context;
    private readonly IImageUploadService _imageUploadService;

    public UserAdvertService(Context context, IImageUploadService imageUploadService)
    {
        _context = context;
        _imageUploadService = imageUploadService;
    }

    public async Task<Response.AdvertIdResponse> CreateAsync(AdvertModel model, string? userId)
    {
        if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Description) || string.IsNullOrWhiteSpace(userId) ||
            string.IsNullOrWhiteSpace(model.Brand) || string.IsNullOrWhiteSpace(model.Model) || !Enum.TryParse(model.ProblemType, out ProblemType problemType))
            throw new BadRequestException(DomainErrors.Advert.OneOrMoreRequiredFlieldsUnspecified);

        if (model.EndDate < DateTime.UtcNow)
            throw new BadRequestException(DomainErrors.Advert.InvalidAdvertEndDate);

        if (model.Photos?.Count > 5)
            throw new BadRequestException(DomainErrors.Advert.TooMuchImages);

        List<string> imagesUrls = new();
        if (model.Photos != null)
        {
            foreach (var photo in model.Photos)
            {
                var imageUrl = await _imageUploadService.Upload(photo);
                imagesUrls.Add(imageUrl);
            }
        }

        var advert = new Domain.Aggregates.Adverts.Advert(
            userId,
            model.Title,
            problemType,
            model.Description,
            model.Brand,
            model.Model,
            model.ManufactureYear,
            imagesUrls,
            model.IsQuestionsFormType,
            model.IsSoundBad,
            model.IsScentBad,
            model.IsPanelInvalid,
            model.IsLeakedLiquids,
            model.IsUnstableCar,
            model.EndDate);

        await _context.AddAsync(advert);
        await _context.SaveChangesAsync();

        return advert.ToAdvertIdModel();
    }

    public async Task<List<Response.UserAdvertResponse>> GetListAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ApplicationException();

        var adverts = await _context.Adverts
            .Where(x => x.UserId == id)
            .Select(x => x.ToUserAdvertModel(x.Jobs.Any(x => x.Status != JobStatus.Pending)))
            .ToListAsync();

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

        var jobs = await _context.Jobs.Where(x => x.AdvertId == id).ToListAsync();

        foreach (var job in jobs)
        {
            if (job.Status != Domain.Enums.JobStatus.Declined)
                throw new BadRequestException(DomainErrors.Advert.ActiveJobs);
        }

        _context.Remove(advert);
        await _context.SaveChangesAsync();
    }
}

