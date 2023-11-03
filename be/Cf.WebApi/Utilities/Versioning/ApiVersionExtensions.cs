using Asp.Versioning;

namespace Cf.WebApi.Utilities.Versioning;

public static class ApiVersionExtensions
{
    /// <summary>
    /// Get API versions.
    /// </summary>
    /// <param name="app">Web application</param>
    /// <returns>List of defined api versions</returns>
    public static IEnumerable<string> GetVersions(this WebApplication app) => app
        .DescribeApiVersions()
        .Select(x => x.GroupName)
        .ToList();

    /// <summary>
    /// Add API versioning.
    /// </summary>
    /// <param name="services"></param>
    public static void AddCustomApiVersioning(this IServiceCollection services) => services
        .AddProblemDetails()
        .AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        })
        .EnableApiVersionBinding();
}

