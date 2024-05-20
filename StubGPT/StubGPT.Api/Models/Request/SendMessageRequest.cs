namespace StubGPT.Api;
public class SendMessageRequest
{
    #region Properties..
    public List<object>? Conversation { get; set; }
    public string Message { get; set; } = null!;
    public string? RolePreamble { get; set; }
    #endregion Properties..
}
