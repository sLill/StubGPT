namespace StubGPT.Database;
public class UserRepository : RepositoryBase, IUserRepository
{
    #region Fields..
    private readonly MainDbContext _mainDbContext;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public UserRepository(ILogger<UserRepository> logger, MainDbContext mainDbContext)
    : base(logger, mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }
    #endregion Constructors..

    #region Methods..	
    public async Task<User?> AddUserAsync(string username, string passwordHash)
    {
        User? newUser = null;   

        var existingUser = await _mainDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
        if (existingUser == null) 
        { 
            newUser = new User() { Username = username, PasswordHash = passwordHash };
            await _mainDbContext.Users.AddAsync(newUser);
        }
     
        return newUser;
    }

    public User? GetUserBySessionToken(string sessionToken)
        => _mainDbContext.Users.FirstOrDefault(x => x != null && x.SessionToken == sessionToken);

    public async Task<User?> GetUserByUsernameAsync(string username)
        => await _mainDbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
    #endregion Methods..
}
