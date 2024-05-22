namespace StubGPT.Database;
public abstract class EntityBase
{
    #region Properties..
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ModifiedOnUtc { get; set; }
    #endregion Properties..
}
