
namespace StubGPT.Core;
public interface IUserService
{
    #region Methods..
    UserConfiguration AddUserConfiguration(Guid userId);
    User? GetUserBySessionToken(string sessionToken);
    Task<User?> GetUserByUsernameAsync(string username);
    UserConfiguration? GetUserConfiguration(Guid userId);
    Task<UserConfiguration?> GetUserConfigurationAsync(Guid userId);
    Task<List<UserPrompt>?> GetUserPromptsAsync(Guid userId);
    Task<string> TryAuthenticateAsync(string username, string password);
    #endregion Methods..
}
