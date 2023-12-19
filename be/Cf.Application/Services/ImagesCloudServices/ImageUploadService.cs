using Cf.Application.Services.Interfaces;
using Cf.Domain.Exceptions;
using Cf.Domain.Exceptions.Messages;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace Cf.Application.Services.ImageUploadServices;

public class ImageUploadService : IImageUploadService
{
    private readonly Cloudinary _cloudinary;

    public ImageUploadService(IConfiguration configuration)
    {
        Account account = new(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]
        );
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> Upload(string imageBase64)
    {
        try
        {
            var imageBytes = Convert.FromBase64String(imageBase64);

            using MemoryStream stream = new(imageBytes);
            ImageUploadParams uploadParams = new()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), stream)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.AbsoluteUri;
        }
        catch
        {
            throw new BadRequestException(DomainErrors.Advert.ImageUploadFailed);
        }
    }
}
