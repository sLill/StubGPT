namespace StubGPT.Database;
public class UserConfiguration : EntityBase
{
    #region Properties..
    [Key]
    public Guid UserConfigurationId { get; set; }

    [StringLength(255)]
    public string? OpenAI_ApiKey { get; set; }

    [StringLength(255)]
    public string? OpenAI_OrganizationId { get; set; }

    [StringLength(255)]
    public string? OpenAI_ProjectId { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    #endregion Properties..
}
