namespace StubGPT.Core;
public interface ICryptographyService
{
    #region Methods..
    string Encrypt(string value);
    string Decrypt(string encryptedValue);
    #endregion Methods..
}
