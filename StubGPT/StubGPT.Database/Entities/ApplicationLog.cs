namespace StubGPT.Database;
public class ApplicationLog : EntityBase
{
    #region Properties..
    [Key]
    public Guid LogId { get; set; }
    public string LogLevel { get; set; } = null!;
    public int EventId { get; set; }
    public string Category { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string? Exception { get; set; }
    #endregion Properties..
}
