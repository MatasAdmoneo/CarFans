
using System.Security.Cryptography;
using Cf.Application.Services.Interfaces;
using Cf.Domain.Aggregates.Services;
using Cf.Domain.Exceptions;
using Cf.Domain.Exceptions.Messages;
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

    public async Task SavePdfAsync(string? base64Content, string? serviceId)
    {
        if (string.IsNullOrEmpty(base64Content))
        {
            throw new BadRequestException(DomainErrors.Service.NotFound);
        }

        if (serviceId is null)
            throw new ApplicationException();

        byte[] pdfBytes;
        try
        {
            pdfBytes = Convert.FromBase64String(base64Content);
        }
        catch (Exception)
        { 
            throw new InternalException(DomainErrors.Service.FailedUpload);
        }
        
        var service = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
        
        if (service is null)
        {
            var newService = new Service(serviceId, pdfBytes);

            await _context.Services.AddAsync(newService);
        }

        else
            service.AddData(pdfBytes);

        await _context.SaveChangesAsync();
    }
}

