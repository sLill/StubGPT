using StubGPT.Database;

namespace StubGPT.Api;
public static class MessageEndpoints
{
    #region Fields..
    private static string _tag = "Message";
    private static string _basePath = "/api/v1/message";
    #endregion Fields..

    #region Methods..
    public static void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints.GetLastSystemPrompt();
        endpoints.SendMessage();
    }

    public static void GetLastSystemPrompt(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet($"{_basePath}/getLastSystemPrompt", async (HttpContext context) =>
        {
            var httpStatusCode = HttpStatusCode.OK;
            object? responseData = null;

            var user = context.Items["User"] as User;
            responseData = new GetLastSystemPromptResponse() { Prompt = user.Configuration!.LastSystemPrompt };

            return Results.Json(responseData, statusCode: (int)httpStatusCode);
        })
        .RequireAuthorization()
        .Produces<GetLastSystemPromptResponse>()
        .WithName(nameof(GetLastSystemPrompt))
        .WithTags(_tag);
    }

    public static void SendMessage(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{_basePath}/sendMessage", async (HttpContext context,
                                                            [FromServices] IChatApiService chatApiService,
                                                            [FromBody] SendMessageRequest data) =>
        {
            var httpStatusCode = HttpStatusCode.OK;
            object? responseData = null;

            string? response = await chatApiService.SendMessageAsync(data.Message, data.Conversation);

            if (response == null)
                httpStatusCode = HttpStatusCode.InternalServerError;
            else 
                responseData = new SendMessageResponse() { Response = response };

            return Results.Json(responseData, statusCode: (int)httpStatusCode);
        })
        .RequireAuthorization()
        .Produces<SendMessageResponse>()
        .WithName(nameof(SendMessage))
        .WithTags(_tag);
    }
    #endregion Methods..
}
