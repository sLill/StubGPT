namespace StubGPT.Core;
public class AuthenticationConfiguration
{
    #region Properties..
    public string Issuer { get; set; } = @"https://www.stubgpt.com/";
    public string Audience { get; set; } = @"https://www.stubgpt.com/";
    public string IssuerSigningKey { get; set; } = "[ISSUER-SIGNING-KEY]";
    public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromDays(30);
    #endregion Properties..
}
