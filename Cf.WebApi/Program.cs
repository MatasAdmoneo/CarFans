using Cf.Infrastucture;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddDbContext<CfContext>(options =>
{
    var connectionString = configuration["ConnectionStrings:CfDatabase"];
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetService<CfContext>();
await db.Database.MigrateAsync();

app.Run();

