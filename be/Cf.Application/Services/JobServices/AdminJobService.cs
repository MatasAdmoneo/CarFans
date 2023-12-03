using Cf.Application.Services.Interfaces;
using Cf.Contracts.Mappers;
using Cf.Domain.Exceptions;
using Cf.Domain.Exceptions.Messages;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cf.Application.Services.JobServices;

public class AdminJobService : IAdminJobService
{
    private readonly Context _context;
    public AdminJobService(Context context)
    {
        _context = context;
    }

    public async Task DeleteAsync(Guid id)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);

        if (job == null)
            throw new BadRequestException(DomainErrors.Job.NotFound);

        _context.Remove(job);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Contracts.Responses.Response.JobIdResponse>> GetListAsync(Guid advertId)
    {
        var jobs = await _context.Jobs.Where(x => x.AdvertId == advertId).ToListAsync();

        return jobs.Select(x => x.ToModel()).ToList();
    }
}

