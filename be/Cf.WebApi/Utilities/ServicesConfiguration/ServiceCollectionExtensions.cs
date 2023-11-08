using Cf.Application.AdvertServices;
using Cf.Application.Interfaces;
using Cf.Application.JobServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //})
        //    .AddCookie()
        //    .AddOpenIdConnect("Auth0", options =>
        //    {
        //        options.Authority = $"https://{configuration["Auth0:Domain"]}";
        //        options.ClientId = configuration["Auth0:ClientId"];
        //        options.ClientSecret = configuration["Auth0:ClientSecret"];
        //        options.ResponseType = OpenIdConnectResponseType.Code;
        //        options.Scope.Clear();
        //        options.Scope.Add("openid");
        //        options.CallbackPath = new PathString("/callback");
        //        options.ClaimsIssuer = "Auth0";
        //    });
        services.AddScoped<IAdvertService, AdvertService>();
        services.AddScoped<IJobService, JobService>();

    }
}

