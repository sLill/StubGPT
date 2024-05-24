
namespace StubGPT.Core;
public interface IUserService
{
    #region Methods..
    Task<User?> AddUserAsync(string username, string password);
    UserConfiguration AddUserConfiguration(Guid userId);
    User? GetUserBySessionToken(string sessionToken);
    Task<User?> GetUserByUsernameAsync(string username);
    UserConfiguration? GetUserConfiguration(Guid userId);
    Task<UserConfiguration?> GetUserConfigurationAsync(Guid userId);
    Task<List<UserPrompt>?> GetUserPromptsAsync(Guid userId);
    Task<string?> TryAuthenticateAsync(string username, string password);
    Task<bool> UpdateUserConfigurationAsync(UserConfiguration userConfiguration);
    #endregion Methods..
}
