namespace StubGPT.Database;
public abstract class RepositoryBase : IRepositoryBase
{
    #region Fields..
    protected readonly ILogger<RepositoryBase> _logger;
    protected readonly DbContext _dbContext;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public RepositoryBase(ILogger<RepositoryBase> logger, DbContext DbContext)
    {
        _logger = logger;
        _dbContext = DbContext;
    }
    #endregion Constructors..

    #region Methods..
    public virtual async Task<bool> AddAsync(object entity)
    {
        bool success = true;

        try
        {
            await _dbContext.AddAsync(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} - {ex.StackTrace}");
            success = false;
        }

        await _dbContext.SaveChangesAsync();
        return success;
    }

    public virtual async Task<bool> AddRangeAsync(IEnumerable<object> entities, bool suspendSaveChanges = false)
    {
        bool success = true;

        try
        {
            await _dbContext.AddRangeAsync(entities);

            if (!suspendSaveChanges)
                await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} - {ex.StackTrace}");
            success = false;
        }

        return success;
    }

    public virtual async Task<int> SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();

    public virtual async Task<bool> UpdateAsync(object entity)
    {
        bool success = true;

        try
        {
            _dbContext.Update(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} - {ex.StackTrace}");
            success = false;
        }

        await _dbContext.SaveChangesAsync();
        return success;
    }
    #endregion Methods..
}
