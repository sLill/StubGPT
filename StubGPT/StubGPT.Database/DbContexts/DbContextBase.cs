﻿namespace StubGPT.Database;
public abstract class DbContextBase : DbContext
{
    #region Fields..
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public DbContextBase(DbContextOptions options)
        : base(options) { }
    #endregion Constructors..

    #region Methods..
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateBaseFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateBaseFields();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateBaseFields();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateBaseFields()
    {
        var modelEntries = ChangeTracker.Entries()
            .Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entry in modelEntries)
        {
            ((EntityBase)entry.Entity).ModifiedOnUtc = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
                ((EntityBase)entry.Entity).CreatedOnUtc = DateTime.UtcNow;
        }
    }
    #endregion Methods..
}
