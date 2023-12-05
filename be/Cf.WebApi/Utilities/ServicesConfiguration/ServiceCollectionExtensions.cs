using Cf.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Cf.Application.Services.AdvertServices;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Cf.WebApi.Utilities.Filters;
using Cf.Application.Services.Interfaces;
using Cf.Application.Services.JobServices;
using Cf.Application.Services.DocumentServices;
using Cf.Application.Services.ServiceInfoServices.DocumentServices;
using Cf.Application.Services.ServiceInformation;

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
        services.AddScoped<IUserAdvertService, UserAdvertService>();
        services.AddScoped<IServiceAdvertService, ServiceAdvertService>();
        services.AddScoped<IAdminAdvertService, AdminAdvertService>();
        services.AddScoped<IAdminJobService, AdminJobService>();
        services.AddScoped<IUserJobService, UserJobService>();
        services.AddScoped<IServiceJobService, ServiceJobService>();
        services.AddScoped<IServiceDocumentService, ServiceDocumentService>();
        services.AddScoped<IAdminDocumentService, AdminDocumentService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IServiceWorkingDaysService, ServiceWorkingDaysService>();
        services.AddScoped<IServicelnfoService, ServiceInfoService>();
        

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = configuration["Authentication:Domain"];
            options.Audience = configuration["Authentication:Audience"];
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                RoleClaimType = "https://CarFans.com/roles"
            };
        });

        services.AddAuthorization(options => { });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1",
                    Description = "A REST API",
                    TermsOfService = new Uri("https://lmgtfy.com/?q=i+like+pie")
                });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "Open Id" }
                        },
                        AuthorizationUrl = new Uri(configuration["Authentication:Domain"] + "authorize?audience=" + configuration["Authentication:Audience"])
                    }
                }
            });

            c.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        var origins = configuration["CorsPolicy:OriginAllowance"];
        services.AddCors(options =>
        {
            options.AddPolicy(name: origins!,
                              builder =>
                              {
                                  builder.WithOrigins("https://localhost:3000")
                                  .WithMethods("GET", "POST","PUT","DELETE")
                                  .AllowAnyHeader();
                              });
        });
    }
}

