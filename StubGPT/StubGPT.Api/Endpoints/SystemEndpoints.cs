
namespace StubGPT.Api;
public static class SystemEndpoints
{
    #region Fields..
    private static string _tag = "System";
    private static string _basePath = "/api/v1/system";
    #endregion Fields..

    #region Methods..
    public static void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints.Ping();
    }

    public static void Ping(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet($"{_basePath}/ping", () =>
        {
            var httpStatusCode = HttpStatusCode.OK;
            return Results.Json(null, statusCode: (int)httpStatusCode);
        })
        .AllowAnonymous()
        .WithName(nameof(Ping))
        .WithTags(_tag);
    }
    #endregion Methods..
}
