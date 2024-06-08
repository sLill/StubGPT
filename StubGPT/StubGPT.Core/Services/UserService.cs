namespace StubGPT.Core;
public class UserService : IUserService
{
    #region Fields..
    private readonly ILogger<UserService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenService _tokenService;
    private readonly ICryptographyService _cryptographyService;
    private readonly IUserRepository _userRepository;
    private readonly IUserSessionRepository _userSessionRepository;
    private readonly IUserConfigurationRepository _userConfigurationRepository;
    private readonly IUserPromptRepository _userPromptRepository;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public UserService(ILogger<UserService> logger, IHttpContextAccessor httpContextAccessor, ITokenService tokenService, ICryptographyService cryptographyService, IUserRepository userRepository, 
        IUserSessionRepository userSessionRepository, IUserConfigurationRepository userConfigurationRepository, IUserPromptRepository userPromptRepository)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _tokenService = tokenService;
        _cryptographyService = cryptographyService;
        _userRepository = userRepository;
        _userSessionRepository = userSessionRepository;
        _userConfigurationRepository = userConfigurationRepository;
        _userPromptRepository = userPromptRepository;
    }
    #endregion Constructors..

    #region Methods..	
    public async Task<User?> AddUserAsync(string username, string password)
    {
        string passwordHash = _cryptographyService.Encrypt(password);
        return await _userRepository.AddUserAsync(username, passwordHash);
    }

    public UserConfiguration AddUserConfiguration(Guid userId)
        => _userConfigurationRepository.AddUserConfiguration(userId);

    public async Task<bool> AddUserPromptAsync(Guid userId, PromptType promptType, string shortcut, string text)
        => await _userPromptRepository.AddAsync(new UserPrompt() { UserId = userId, PromptType = promptType, Shortcut = shortcut, Text = text });

    public UserSession? GetUserSession(string sessionToken)
        => _userSessionRepository.GetUserSession(sessionToken);

    public async Task<User?> GetUserByUsernameAsync(string username)
        => await _userRepository.GetUserByUsernameAsync(username);

    public UserConfiguration? GetUserConfiguration(Guid userId)
    => _userConfigurationRepository.GetUserConfiguration(userId);

    public async Task<UserConfiguration?> GetUserConfigurationAsync(Guid userId)
        => await _userConfigurationRepository.GetUserConfigurationAsync(userId);

    public async Task<List<UserPrompt>?> GetSavedPromptsAsync(Guid userId)
        => await _userPromptRepository.GetUserPromptsAsync(userId);

    public async Task<bool> RemoveUserPromptAsync(UserPrompt prompt)
        => await _userPromptRepository.RemoveAsync(prompt);

    public async Task<string?> TryAuthenticateAsync(string username, string password)
    {
        string? sessionToken = null;

        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user != null)
        {
            string encryptedPassword = _cryptographyService.Encrypt(password);
            if (encryptedPassword == user.PasswordHash)
            {
                // JWT: Build claims
                var claims = new List<Claim>();

                if (user.IsAdmin)
                    claims.Add(new Claim(UserClaim.IsAdmin.ToString(), "true"));

                // JWT: Build token
                sessionToken = _tokenService.GenerateToken(user.UserId, claims);

                await _userSessionRepository.AddUserSessionAsync(user, sessionToken);
            }
        }

        if (sessionToken != null) 
            _logger.LogInformation($"Login Success ({_httpContextAccessor.HttpContext.Connection.RemoteIpAddress}) - Username: {username}");
        else
            _logger.LogError($"Login Failed ({_httpContextAccessor.HttpContext.Connection.RemoteIpAddress}) - Username: {username}");

        return sessionToken;
    }

    public async Task<bool> UpdateUserConfigurationAsync(UserConfiguration userConfiguration)
        => await _userConfigurationRepository.UpdateAsync(userConfiguration);

    public async Task<bool> UpdateUserPromptAsync(UserPrompt userPrompt)
        => await _userPromptRepository.UpdateAsync(userPrompt); 
    #endregion Methods..
}
