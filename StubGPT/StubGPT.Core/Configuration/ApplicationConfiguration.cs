namespace StubGPT.Core;
public class ApplicationConfiguration
{
    #region Properties..
    public string SessionToken_Issuer { get; set; } = @"https://www.stubgpt.com/";
    public string SessionToken_Audience { get; set; } = @"https://www.stubgpt.com/";
    public string? SessionToken_IssuerSigningKey { get; set; }
    public int SessionToken_Lifetime_Days { get; set; } = 30;

    public string? Encryption_IV { get; set; }
    public string? Encryption_Key { get; set; }
    #endregion Properties..
}
