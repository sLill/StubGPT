namespace StubGPT.Database;
public class UserPrompt : EntityBase
{
    #region Properties..
    [Key]
    public Guid UserPromptId { get; set; }
    public PromptType PromptType { get; set; }

    [StringLength(32)]
    public string? Shortcut { get; set; }
    public string? Text { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    #endregion Properties..
}
