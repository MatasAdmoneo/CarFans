namespace Cf.WebApi.Routing;

public static class IEndpointRouteBuilderExtensions
{
    /// <summary>
    /// Represents an empty route. For example if the group is already tagged
    /// with tag, then the methods in this extensions class will map CRUD methods to path /tag/.
    /// </summary>
    public static readonly string Pattern = string.Empty;

    /// <summary>
    /// Maps Get method without route.
    /// </summary>
    /// <param name="builder">Endpoint builder</param>
    /// <param name="handler">Handler</param>
    /// <returns>Route builder</returns>
    public static RouteHandlerBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler) =>
        builder.MapGet(Pattern, handler);

    /// <summary>
    /// Maps Put method without route.
    /// </summary>
    /// <param name="builder">Endpoint builder</param>
    /// <param name="handler">Handler</param>
    /// <returns>Route builder</returns>
    public static RouteHandlerBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler) =>
        builder.MapPut(Pattern, handler);

    /// <summary>
    /// Maps Post method without route.
    /// </summary>
    /// <param name="builder">Endpoint builder</param>
    /// <param name="handler">Handler</param>
    /// <returns>Route builder</returns>
    public static RouteHandlerBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler) =>
        builder.MapPost(Pattern, handler);

    /// <summary>
    /// Maps Patch method without route.
    /// </summary>
    /// <param name="builder">Endpoint builder</param>
    /// <param name="handler">Handler</param>
    /// <returns>Route builder</returns>
    public static RouteHandlerBuilder MapPatch(this IEndpointRouteBuilder builder, Delegate handler) =>
        builder.MapPatch(Pattern, handler);

    /// <summary>
    /// Maps MapDelete method without route.
    /// </summary>
    /// <param name="builder">Endpoint builder</param>
    /// <param name="handler">Handler</param>
    /// <returns>Route builder</returns>
    public static RouteHandlerBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler) =>
        builder.MapDelete(Pattern, handler);
}

