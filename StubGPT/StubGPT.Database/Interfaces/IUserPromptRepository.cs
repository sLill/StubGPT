namespace StubGPT.Database;
public interface IUserPromptRepository
{
    #region Methods..
    Task<List<UserPrompt>?> GetUserPromptsAsync(Guid userId);
    #endregion Methods..
}
