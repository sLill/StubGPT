namespace StubGPT.Core;
public class ChatGPTChatResponse
{
    #region Properties..
    public Choice[]? choices { get; set; } 
    #endregion Properties..
}

public class Choice
{
    public Message message { get; set; }
}

public class Message
{
    public string role { get; set; }
    public string content { get; set; }
}
