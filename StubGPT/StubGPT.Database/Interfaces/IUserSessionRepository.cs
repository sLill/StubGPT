namespace StubGPT.Database;
public interface IUserSessionRepository : IRepositoryBase
{
    #region Methods..
    Task<UserSession?> AddUserSessionAsync(User user, string sessionToken);
    UserSession? GetUserSession(string sessionToken);
    #endregion Methods..
}
