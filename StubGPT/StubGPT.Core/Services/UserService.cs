namespace StubGPT.Core;
public class UserService : IUserService
{
    #region Fields..
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IUserConfigurationRepository _userConfigurationRepository;
    private readonly IUserPromptRepository _userPromptRepository;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public UserService(ILogger<UserService> logger, IUserRepository userRepository, 
        IUserConfigurationRepository userConfigurationRepository, IUserPromptRepository userPromptRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _userConfigurationRepository = userConfigurationRepository;
        _userPromptRepository = userPromptRepository;
    }
    #endregion Constructors..

    #region Methods..	
    public UserConfiguration AddUserConfiguration(Guid userId)
        => _userConfigurationRepository.AddUserConfiguration(userId);

    public User? GetUserBySessionToken(string sessionToken)
        => _userRepository.GetUserBySessionToken(sessionToken);

    public async Task<User?> GetUserByUsernameAsync(string username)
        => await _userRepository.GetUserByUsernameAsync(username);

    public UserConfiguration? GetUserConfiguration(Guid userId)
    => _userConfigurationRepository.GetUserConfiguration(userId);

    public async Task<UserConfiguration?> GetUserConfigurationAsync(Guid userId)
        => await _userConfigurationRepository.GetUserConfigurationAsync(userId);

    public async Task<List<UserPrompt>?> GetUserPromptsAsync(Guid userId)
        => await _userPromptRepository.GetUserPromptsAsync(userId);

    public async Task<string> TryAuthenticateAsync(string username, string password)
    {

    }
    #endregion Methods..
}
