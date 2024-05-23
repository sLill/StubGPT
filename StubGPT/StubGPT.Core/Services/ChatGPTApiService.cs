namespace StubGPT.Core;
public class ChatGPTApiService : IChatApiService
{
    #region Fields..
    private const string BASE_URL = @"https://api.openai.com";
    private const string VERSION = "v1";

    private readonly IHttpContextAccessor _httpContextAccessor;
    #endregion Fields..

    #region Properties..
    public ChatGPTModel Model { get; set; } = ChatGPTModel.GPT4o;
    #endregion Properties..

    #region Constructors..
    public ChatGPTApiService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    #endregion Constructors..

    #region Methods..			
    public async Task<string?> SendMessageAsync(string? message, List<object>? conversation)
    {
        string? result = null;

        conversation = conversation ?? new List<object>();

        if (!string.IsNullOrEmpty(message))
            conversation.Add(new { role = "user", content = message });

        if (conversation.Any())
        {
            var requestBody = new
            {
                model = Model.GetCustomAttribute<ValueAttribute>()!.Value,
                messages = conversation
            };

            var response = await PostAsync("/chat/completions", requestBody);
            if (response?.StatusCode == HttpStatusCode.OK)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var chatGPTResponse = JsonSerializer.Deserialize<ChatGPTChatResponse>(responseJson);
                if (chatGPTResponse!.choices?.Any() ?? false)
                    result = chatGPTResponse.choices[0].message!.content;
            }
        }

        return result;
    }

    private async Task<HttpResponseMessage?> GetAsync(string relativeEndpoint)
    {
        HttpResponseMessage? response = null;

        try
        {
            var user = _httpContextAccessor.HttpContext?.Items["User"] as User;
            if (user != null)
            {
                string requestUri = $"{BASE_URL}/{VERSION}/{relativeEndpoint.Trim('\\', '/')}";
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {user.Configuration!.OpenAI_ApiKey}");
                    httpClient.DefaultRequestHeaders.Add("OpenAI-Organization", user.Configuration.OpenAI_OrganizationId);
                    httpClient.DefaultRequestHeaders.Add("OpenAI-Project", user.Configuration.OpenAI_ProjectId);

                    response = await httpClient.GetAsync(requestUri);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(ChatGPTApiService)}.{nameof(GetAsync)}: {ex.Message} - {ex.StackTrace}");
        }

        return response;
    }

    private async Task<HttpResponseMessage?> PostAsync(string relativeEndpoint, object requestBody)
    {
        HttpResponseMessage? response = null;
        
        try
        {
            var user = _httpContextAccessor.HttpContext?.Items["User"] as User;
            if (user != null)
            {
                string requestUri = $"{BASE_URL}/{VERSION}/{relativeEndpoint.Trim('\\', '/')}";
                var requestBodyJson = JsonSerializer.Serialize(requestBody);
                var httpContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {user.Configuration!.OpenAI_ApiKey}");
                    httpClient.DefaultRequestHeaders.Add("OpenAI-Organization", user.Configuration.OpenAI_OrganizationId);
                    httpClient.DefaultRequestHeaders.Add("OpenAI-Project", user.Configuration.OpenAI_ProjectId);

                    response = await httpClient.PostAsync(requestUri, httpContent);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(ChatGPTApiService)}.{nameof(PostAsync)}: {ex.Message} - {ex.StackTrace}");
        }

        return response;
    }
    #endregion Methods..
}
