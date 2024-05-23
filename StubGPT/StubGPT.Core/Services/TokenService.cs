namespace StubGPT.Core;
public class TokenService : ITokenService
{
    #region Fields..
    private IOptionsSnapshot<ApplicationConfiguration> _authenticationConfiguration;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public TokenService(IOptionsSnapshot<ApplicationConfiguration> authenticationConfiguration)
    {
        _authenticationConfiguration = authenticationConfiguration;
    }
    #endregion Constructors..

    #region Methods..			
    public string GenerateToken(Guid userId, List<Claim> customClaims)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationConfiguration.Value.SessionToken_IssuerSigningKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, userId.ToString()),
        };

        claims.AddRange(customClaims);

        var jwtSecurityToken = new JwtSecurityToken(issuer: _authenticationConfiguration.Value.SessionToken_Issuer,
                                                    audience: _authenticationConfiguration.Value.SessionToken_Audience,
                                                    claims: claims,
                                                    expires: DateTime.Now.AddMinutes(TimeSpan.FromDays(_authenticationConfiguration.Value.SessionToken_Lifetime_Days).TotalMinutes),
                                                    signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
    #endregion Methods..
}
