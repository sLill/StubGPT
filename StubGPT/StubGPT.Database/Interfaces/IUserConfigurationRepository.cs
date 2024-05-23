namespace StubGPT.Database;
public interface IUserConfigurationRepository : IRepositoryBase
{
    #region Methods..
    UserConfiguration AddUserConfiguration(Guid userId);
    UserConfiguration? GetUserConfiguration(Guid userId);
    Task<UserConfiguration?> GetUserConfigurationAsync(Guid userId);
    #endregion Methods..
}
