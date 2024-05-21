namespace StubGPT.Core;
public interface IChatApiService
{
    #region Methods..
    Task<string?> SendMessageAsync(string message, List<object>? conversation);
    #endregion Methods..
}
