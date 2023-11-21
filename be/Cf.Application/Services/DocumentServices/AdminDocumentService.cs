using Cf.Application.Services.Interfaces;
using Cf.Contracts.Mappers;
using Cf.Contracts.Responses;
using Cf.Domain.Enums;
using Cf.Domain.Exceptions;
using Cf.Domain.Exceptions.Messages;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace Cf.Application.Services.DocumentServices;

public class AdminDocumentService : IAdminDocumentService
{
    private readonly Context _context;

    public AdminDocumentService(Context context)
    {
        _context = context;
    }

    public async Task<List<Response.ServiceInfo>> GetServicesAsync()
    {
        var services = await _context.Services.OrderBy(x => x.CreatedDate).ToListAsync();

        return services.Select(x => x.ToModel()).ToList();
    }

    public async Task UpdateStatusAsync(string serviceId, ServiceStatus status)
    {
        var service = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);

        if (service == null)
            throw new ApplicationException();

        service.Status = status;

        await _context.SaveChangesAsync();
    }

    public async Task<byte[]> GetPdfsAsync(string serviceId)
    {
        var service = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);

        if (service != null && service.Data.Any())
        {
            // For simplicity, this example zips all PDFs associated with the service
            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    for (int i = 0; i < service.Data.Count; i++)
                    {
                        var entry = zipArchive.CreateEntry($"pdf_{i + 1}.pdf");

                        using (var entryStream = entry.Open())
                        {
                            await entryStream.WriteAsync(service.Data[i], 0, service.Data[i].Length);
                        }
                    }
                }
                return memoryStream.ToArray();
            }
        }

        throw new ApplicationException("PDFs not found for the specified serviceId.");
    }
}

