namespace StubGPT.Database;
public class UserSession : EntityBase
{
    #region Properties..
    [Key]
    public Guid UserSessionId { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string? SessionToken { get; set; }
    #endregion Properties..
}
