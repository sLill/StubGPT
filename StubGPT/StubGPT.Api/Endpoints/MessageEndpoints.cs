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
        endpoints.SendMessage();
    }

    public static void SendMessage(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{_basePath}/sendMessage", async ([FromServices] IChatApiService chatApiService,
                                                             [FromBody] SendMessageRequest data) =>
        {
            var httpStatusCode = HttpStatusCode.OK;
            object? responseData = null;

            string? response = null;
            if (data.Conversation?.Any() ?? false)
                response = await chatApiService.SendMessageAsync(data.Message, data.Conversation);
            else
                response = await chatApiService.SendMessageAsync(data.Message);

            if (response == null)
                httpStatusCode = HttpStatusCode.InternalServerError;
            else 
                responseData = new SendMessageResponse() { Response = response };

            return Results.Json(responseData, statusCode: (int)httpStatusCode);
        })
        .AllowAnonymous()
        .Produces<SendMessageResponse>()
        .WithName(nameof(SendMessage))
        .WithTags(_tag);
    }
    #endregion Methods..
}
