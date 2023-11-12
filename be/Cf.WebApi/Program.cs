using Cf.Infrastructure;
using Cf.WebApi.Endpoints;
using Cf.WebApi.Utilities.ServicesConfiguration;
using Cf.WebApi.Utilities.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;
var configuration = builder.Configuration;

services.ConfigureJsonOptions();
services.ConfigureServices(configuration);
services.AddCustomApiVersioning();


services.AddDbContext<Context>(options =>
{
    options.UseNpgsql(configuration["ConnectionStrings:Database"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
        c.OAuthClientId(configuration["Authentication:ClientId"]);
    });
}

app.UseHttpsRedirection();

var api = app
    .NewVersionedApi("Cf")
    .MapGroup("v{version:apiVersion}");

api.MapAdminAdvertRoutes();
api.MapAdminJobRoutes();

api.MapServiceAdvertRoutes();
api.MapServiceJobRoutes();

api.MapUserAdvertRoutes();
api.MapUserJobRoutes();



await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetService<Context>();
await db.Database.MigrateAsync();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

