namespace Cf.Application.Services.Interfaces;

public interface IImageUploadService
{
    Task<string> Upload(string imageBase64);
}