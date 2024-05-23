namespace StubGPT.Database;
public class User : EntityBase
{
    #region Properties..
    [Key]
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? SessionToken { get; set; }
    public bool IsAdmin { get; set; }

    public UserConfiguration? Configuration { get; set; }
    #endregion Properties..
}
