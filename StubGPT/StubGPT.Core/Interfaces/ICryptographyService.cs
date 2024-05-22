namespace StubGPT.Core;
public interface ICryptographyService
{
    #region Methods..
    byte[] Encrypt(string value);
    string Decrypt(byte[] encryptedBytes);
    #endregion Methods..
}
