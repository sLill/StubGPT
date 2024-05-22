namespace StubGPT.Database;
public interface IUserRepository
{
    #region Methods..
    User? GetUserBySessionToken(string sessionToken);
    Task<User?> GetUserByUsernameAsync(string username);
    #endregion Methods..
}
