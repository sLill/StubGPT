
namespace StubGPT.Core;
public interface IUserService
{
    #region Methods..
    Task<User?> AddUserAsync(string username, string password);
    UserConfiguration AddUserConfiguration(Guid userId);
    Task<bool> AddUserPromptAsync(Guid userId, PromptType promptType, string shortcut, string text);
    UserSession? GetUserSession(string sessionToken);
    Task<User?> GetUserByUsernameAsync(string username);
    UserConfiguration? GetUserConfiguration(Guid userId);
    Task<UserConfiguration?> GetUserConfigurationAsync(Guid userId);
    Task<List<UserPrompt>?> GetSavedPromptsAsync(Guid userId);
    Task<bool> RemoveUserPromptAsync(UserPrompt prompt);
    Task<string?> TryAuthenticateAsync(string username, string password);
    Task<bool> UpdateUserConfigurationAsync(UserConfiguration userConfiguration);
    Task<bool> UpdateUserPromptAsync(UserPrompt userPrompt);
    #endregion Methods..
}
