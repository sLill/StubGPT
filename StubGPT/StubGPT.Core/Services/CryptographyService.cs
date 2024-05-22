namespace StubGPT.Core;
public class CryptographyService : ICryptographyService
{
    #region Fields..
    private const int SALT_BYTES = 8;
    private const int KEY_SIZE = 16;
    private const int ITERATIONS = 1000;
    private const string IV = "HJD445D67727C703DFY8K32ABEA9C41D";
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    #endregion Constructors..

    #region Methods..	
    public byte[] Encrypt(string value)
    {

    }

    public string Decrypt(byte[] encryptedBytes)
    {

    }
    #endregion Methods..
}
