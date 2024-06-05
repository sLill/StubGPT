namespace StubGPT.Api;
public class GetSavedPromptsResponse
{
    #region Properties..
    public List<UserPrompt> Prompts { get; set; } = new List<UserPrompt>();
    #endregion Properties..
}
