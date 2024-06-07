namespace StubGPT.Api;
public class UserMiddleware
{
    #region Fields..
    private readonly RequestDelegate _next;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public UserMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    #endregion Constructors..

    #region Methods..		
    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        var responseStatusCode = HttpStatusCode.Unauthorized;
        var endpoint = context.GetEndpoint();

        var serviceScope = serviceProvider.CreateScope();
        IUserService? userService = serviceScope.ServiceProvider.GetService<IUserService>();
        
        var allowAnonymous = endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>();
        if (allowAnonymous != null)
            responseStatusCode = HttpStatusCode.OK;
        else
        {
            var authorizationHeaderTokens = context?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ");
            if (authorizationHeaderTokens?.Length > 1)
            {
                string? sessionToken = authorizationHeaderTokens.Last();
                if (sessionToken != null)
                {
                    var userSession = userService.GetUserSession(sessionToken);
                    if (userSession != null)
                    {
                        var userConfiguration = userService.GetUserConfiguration(userSession.UserId);

                        if (userConfiguration == null)
                            userConfiguration = userService.AddUserConfiguration(userSession.UserId);

                        userSession.User.Configuration = userConfiguration;

                        context.Items["UserSession"] = userSession;
                        responseStatusCode = HttpStatusCode.OK;
                    }
                }
            }
        }

        context.Response.StatusCode = (int)responseStatusCode;

        if (responseStatusCode == HttpStatusCode.OK)
            await _next(context);
    }
    #endregion Methods..
}
