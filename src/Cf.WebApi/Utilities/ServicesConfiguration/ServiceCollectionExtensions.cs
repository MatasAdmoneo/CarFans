using Cf.Application.AdvertServices;
using Cf.Application.Interfaces;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

namespace Cf.WebApi.Utilities.ServicesConfiguration;

public static class ServiceCollectionExtensions
{
    public static void ConfigureJsonOptions(this IServiceCollection services) =>
    services.Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAdvertService, AdvertService>();
    }
}

