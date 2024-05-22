namespace StubGPT.Core;
public interface ITokenService
{
    #region Methods..
    string GenerateToken(Guid userId, List<Claim> customClaims);
    #endregion Methods..
}
