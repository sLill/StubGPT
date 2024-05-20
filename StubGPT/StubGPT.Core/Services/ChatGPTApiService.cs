namespace StubGPT.Core;
public class ChatGPTApiService : IChatApiService
{
    #region Fields..
    private const string BASE_URL = @"https://api.openai.com";
    private const string VERSION = "v1";

    public ChatGPTModel Model { get; set; } = ChatGPTModel.GPT4o;

    private readonly IOptionsSnapshot<ChatGPTApiConfiguration> _chatGPTApiConfiguration;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public ChatGPTApiService(IOptionsSnapshot<ChatGPTApiConfiguration> chatGPTApiConfiguration)
    {
        _chatGPTApiConfiguration = chatGPTApiConfiguration;
    }
    #endregion Constructors..

    #region Methods..			
    public async Task<string?> SendMessageAsync(string message, string rolePreamble = "You are a helpful assistant")
    {
        var conversation = new List<MessageRecord>() 
        { 
           new MessageRecord() { Role = "system", Content = rolePreamble }
        };

        return await SendMessageAsync(message, conversation);
    }

    public async Task<string?> SendMessageAsync(string message, List<MessageRecord> conversation)
    {
        string? result = null;

        conversation.Add(new MessageRecord() { Role = "user", Content = message });

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
            if (chatGPTResponse!.Choices?.Any() ?? false)
                result = chatGPTResponse.Choices[0].Message!.Content;
        }

        return result;
    }

    private async Task<HttpResponseMessage?> PostAsync(string relativeEndpoint, object requestBody)
    {
        HttpResponseMessage? response = null;
        
        try
        {
            string requestUri =  $"{BASE_URL}/{VERSION}/{relativeEndpoint.Trim('\\', '/')}";
            var requestBodyJson = JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient() )
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_chatGPTApiConfiguration.Value.ApiKey}");
                response = await httpClient.PostAsync(requestUri, httpContent);
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
