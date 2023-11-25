
using Cf.Application.Services.Interfaces;
using Cf.Domain.Aggregates.Services;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cf.Application.Services.DocumentServices;

public class ServiceDocumentService : IServiceDocumentService
{
    private readonly Context _context;

    public ServiceDocumentService(Context context)
    {
        _context = context;
    }

    public async Task SavePdfAsync(byte[] pdfBytes, string? serviceId)
    {
        if (serviceId is null)
            throw new ApplicationException();

        var service = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
        
        if (service is null)
        {
            var newService = new Service(serviceId, pdfBytes);

            await _context.Services.AddAsync(newService);
        }

        else
            service.Data.Add(pdfBytes);

        await _context.SaveChangesAsync();
    }
}

