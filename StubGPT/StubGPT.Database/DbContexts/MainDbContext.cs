namespace StubGPT.Database;
public class MainDbContext : DbContextBase
{
    #region Properties..
    public DbSet<ApplicationLog> ApplicationLogs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserConfiguration> UserConfiguration { get; set; }
    public DbSet<UserPrompt> UserPrompts { get; set; }
    #endregion Properties..

    #region Constructors..
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options) { }
    #endregion Constructors..

    #region Methods..		
    #endregion Methods..
}
