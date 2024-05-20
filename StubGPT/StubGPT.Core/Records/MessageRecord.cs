namespace StubGPT.Core;
public record MessageRecord
{
    public string? Role { get; set; }
    public string? Content { get; set; }
}