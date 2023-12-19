using Cf.Domain.Models;
using Cf.Contracts.Responses;

namespace Cf.Application.Services.Interfaces;

public interface IUserReviewService
{
    Task CreateAsync(string? userId, Guid jobId, ReviewModel model);

    Task<Response.FullReviewInfo> GetByServiceId(string serviceId);
}