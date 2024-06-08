namespace StubGPT.Api;
public static class UserEndpoints
{
    #region Fields..
    private static string _tag = "User";
    private static string _basePath = "/api/v1/user";
    #endregion Fields..

    #region Methods..
    public static void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints.AddSavedPrompt();
        endpoints.UpdateSavedPrompt();
        endpoints.RemoveSavedPrompt();
        endpoints.GetSavedPrompts();
        endpoints.IsAuthenticated();
        endpoints.AddUser();
        endpoints.Login();
    }

    public static void AddSavedPrompt(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{_basePath}/addSavedPrompt", async (HttpContext context, 
                                                                [FromServices] IUserService userService,
                                                                [FromBody] AddSavedPromptRequest data) =>
        {
            var httpStatusCode = HttpStatusCode.OK;

            try
            {
                var userSession = context.Items["UserSession"] as UserSession;

                PromptType promptType = Enum.Parse<PromptType>(data.PromptType);
                bool result = await userService.AddUserPromptAsync(userSession!.UserId, promptType, string.Empty, string.Empty);
                httpStatusCode = result ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            }
            catch 
            {
                httpStatusCode = HttpStatusCode.BadRequest;
            }

            return Results.Json(null, statusCode: (int)httpStatusCode);
        })
       .RequireAuthorization()
       .WithName(nameof(AddSavedPrompt))
       .WithTags(_tag);
    }

    public static void UpdateSavedPrompt(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{_basePath}/updateSavedPrompt", async (HttpContext context,
                                                                   [FromServices] IUserService userService,
                                                                   [FromBody] UpdateSavedPromptRequest data) =>
        {
            var httpStatusCode = HttpStatusCode.OK;

            try
            {
                var userSession = context.Items["UserSession"] as UserSession;

                bool result = await userService.UpdateUserPromptAsync(data.Prompt);
                httpStatusCode = result ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            }
            catch
            {
                httpStatusCode = HttpStatusCode.BadRequest;
            }

            return Results.Json(null, statusCode: (int)httpStatusCode);
        })
       .RequireAuthorization()
       .WithName(nameof(UpdateSavedPrompt))
       .WithTags(_tag);
    }

    public static void RemoveSavedPrompt(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{_basePath}/removeSavedPrompt", async (HttpContext context,
                                                                   [FromServices] IUserService userService,
                                                                   [FromBody] DeleteSavedPromptRequest data) =>
        {
            var httpStatusCode = HttpStatusCode.OK;

            try
            {
                var userSession = context.Items["UserSession"] as UserSession;

                bool result = await userService.RemoveUserPromptAsync(data.Prompt);
                httpStatusCode = result ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            }
            catch
            {
                httpStatusCode = HttpStatusCode.BadRequest;
            }

            return Results.Json(null, statusCode: (int)httpStatusCode);
        })
       .RequireAuthorization()
       .WithName(nameof(RemoveSavedPrompt))
       .WithTags(_tag);
    }

    public static void GetSavedPrompts(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet($"{_basePath}/getSavedPrompts", async (HttpContext context,
                                                               [FromServices] IUserService userService) =>
        {
            var httpStatusCode = HttpStatusCode.OK;
            object? responseData = null;

            var userSession = context.Items["UserSession"] as UserSession;
            var savedPrompts = await userService.GetSavedPromptsAsync(userSession!.UserId) ?? new List<UserPrompt>();

            responseData = new GetSavedPromptsResponse() { Prompts = savedPrompts };

            return Results.Json(responseData, statusCode: (int)httpStatusCode);
        })
        .RequireAuthorization()
        .Produces<GetSavedPromptsResponse>()
        .WithName(nameof(GetSavedPrompts))
        .WithTags(_tag);
    }

    public static void IsAuthenticated(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet($"{_basePath}/isAuthenticated", () =>
        {
            var httpStatusCode = HttpStatusCode.OK;
            object? responseData = null;

            return Results.Json(responseData, statusCode: (int)httpStatusCode);
        })
       .RequireAuthorization()
       .WithName(nameof(IsAuthenticated))
       .WithTags(_tag);
    }

    public static void AddUser(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{_basePath}/addUser", async ([FromServices] IUserService userService,
                                                        [FromBody] AddUserRequest data) =>
        {
            var httpStatusCode = HttpStatusCode.OK;
            object? responseData = null;

            var newUser = await userService.AddUserAsync(data.Username, data.Password);
            if (newUser == null)
                httpStatusCode = HttpStatusCode.BadRequest;
            else
                responseData = new AddUserResponse() { Message = "Success" };

            return Results.Json(responseData, statusCode: (int)httpStatusCode);
        })
       .RequireAuthorization(UserClaim.IsAdmin.ToString())
       .Produces<AddUserResponse>()
       .WithName(nameof(AddUser))
       .WithTags(_tag);
    }

    public static void Login(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{_basePath}/login", async ([FromServices] IUserService userService,
                                                       [FromBody] LoginRequest data) =>
        {
            var httpStatusCode = HttpStatusCode.OK;
            object? responseData = null;

            string? sessionToken = await userService.TryAuthenticateAsync(data.Username, data.Password);
            if (sessionToken == null)
                httpStatusCode = HttpStatusCode.BadRequest;
            else
                responseData = new LoginResponse() { SessionToken = sessionToken };

            return Results.Json(responseData, statusCode: (int)httpStatusCode);
        })
        .AllowAnonymous()
        .Produces<LoginResponse>()
        .WithName(nameof(Login))
        .WithTags(_tag);
    }
    #endregion Methods..
}
