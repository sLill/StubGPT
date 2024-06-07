namespace StubGPT.Database;
public class UserSessionRepository : RepositoryBase, IUserSessionRepository
{
    #region Fields..
    private readonly MainDbContext _mainDbContext;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public UserSessionRepository(ILogger<UserSessionRepository> logger, MainDbContext mainDbContext)
        : base(logger, mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }
    #endregion Constructors..

    #region Methods..	
    public async Task<UserSession?> AddUserSessionAsync(User user, string sessionToken)
    {
        var userSession = new UserSession() { UserId = user.UserId, SessionToken = sessionToken };
        
        await _mainDbContext.UserSessions.AddAsync(userSession);
        await _mainDbContext.SaveChangesAsync();
        
        return userSession;
    }

    public UserSession? GetUserSession(string sessionToken)
    {
        return _mainDbContext.UserSessions
            .Include(x => x.User)
                .ThenInclude(x => x.Configuration)
            .FirstOrDefault(x => x != null && x.SessionToken == sessionToken);
    }
    #endregion Methods..
}
