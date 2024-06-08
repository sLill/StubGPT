namespace StubGPT.Database;
public interface IRepositoryBase
{
    #region Methods..
    Task<bool> AddAsync(object entity);
    Task<bool> AddRangeAsync(IEnumerable<object> entities, bool suspendSaveChanges = false);
    Task<bool> RemoveAsync(object entity);
    Task<int> SaveChangesAsync();
    Task<bool> UpdateAsync(object entity);
    #endregion Methods..
}
