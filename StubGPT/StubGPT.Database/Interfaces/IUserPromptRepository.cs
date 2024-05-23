namespace StubGPT.Database;
public interface IUserPromptRepository : IRepositoryBase
{
    #region Methods..
    Task<List<UserPrompt>?> GetUserPromptsAsync(Guid userId);
    #endregion Methods..
}
