namespace StubGPT.Core;
public class UserService : IUserService
{
    #region Fields..
    private readonly ILogger<UserService> _logger;
    private readonly ITokenService _tokenService;
    private readonly ICryptographyService _cryptographyService;
    private readonly IUserRepository _userRepository;
    private readonly IUserConfigurationRepository _userConfigurationRepository;
    private readonly IUserPromptRepository _userPromptRepository;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public UserService(ILogger<UserService> logger, ITokenService tokenService, ICryptographyService cryptographyService, IUserRepository userRepository, 
        IUserConfigurationRepository userConfigurationRepository, IUserPromptRepository userPromptRepository)
    {
        _logger = logger;
        _tokenService = tokenService;
        _cryptographyService = cryptographyService;
        _userRepository = userRepository;
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
                user.SessionToken = sessionToken;
                await _userRepository.UpdateAsync(user);
            }
        }

        return sessionToken;
    }
    #endregion Methods..
}
