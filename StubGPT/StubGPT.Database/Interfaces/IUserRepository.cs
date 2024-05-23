namespace StubGPT.Database;
public interface IUserRepository : IRepositoryBase
{
    #region Methods..
    Task<User?> AddUserAsync(string username, string passwordHash);
    User? GetUserBySessionToken(string sessionToken);
    Task<User?> GetUserByUsernameAsync(string username);
    #endregion Methods..
}
