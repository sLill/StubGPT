namespace StubGPT.Database;
public class UserPromptRepository: RepositoryBase, IUserPromptRepository
{
    #region Fields..
    private readonly MainDbContext _mainDbContext;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public UserPromptRepository(ILogger<UserPromptRepository> logger, MainDbContext mainDbContext)
    : base(logger, mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }
    #endregion Constructors..

    #region Methods..	
    public async Task<List<UserPrompt>?> GetUserPromptsAsync(Guid userId)
        => await _mainDbContext.UserPrompts.Where(x => x.UserId == userId).OrderBy(x => x.CreatedOnUtc).ToListAsync();
    #endregion Methods..
}
