namespace StubGPT.Core;
public interface IChatApiService
{
    #region Methods..
    Task<string?> SendMessageAsync(string message, string rolePreamble = "You are a helpful assistant");
    Task<string?> SendMessageAsync(string message, List<MessageRecord> conversation);
    #endregion Methods..
}
