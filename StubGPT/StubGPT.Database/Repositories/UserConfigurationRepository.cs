namespace StubGPT.Database;
public class UserConfigurationRepository : RepositoryBase, IUserConfigurationRepository
{
    #region Fields..
    private readonly MainDbContext _mainDbContext;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public UserConfigurationRepository(ILogger<UserConfigurationRepository> logger, MainDbContext mainDbContext)
    : base(logger, mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }
    #endregion Constructors..

    #region Methods..	
    public UserConfiguration AddUserConfiguration(Guid userId)
    {
        var userConfiguration = new UserConfiguration() { UserId = userId };
        _mainDbContext.UserConfiguration.Add(userConfiguration);
        return userConfiguration;
    }

    public UserConfiguration? GetUserConfiguration(Guid userId)
        => _mainDbContext.UserConfiguration.FirstOrDefault(x => x.UserId == userId);

    public async Task<UserConfiguration?> GetUserConfigurationAsync(Guid userId)
        => await _mainDbContext.UserConfiguration.FirstOrDefaultAsync(x => x.UserId == userId);
    #endregion Methods..
}
