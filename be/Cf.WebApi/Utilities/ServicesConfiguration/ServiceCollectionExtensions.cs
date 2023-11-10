﻿using Cf.Application.AdvertServices;
using Cf.Application.Interfaces;
using Cf.Application.JobServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Cf.WebApi.Utilities.Filters;

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
        services.AddScoped<IJobService, JobService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = configuration["Authentication:Domain"];
            options.Audience = configuration["Authentication:Audience"];
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
    }
}
