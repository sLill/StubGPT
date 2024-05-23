namespace StubGPT.Core;
public class CryptographyService : ICryptographyService
{
    #region Fields..
    private readonly ILogger<CryptographyService> _logger;
    private readonly IOptions<ApplicationConfiguration> _applicationConfiguration;
    #endregion Fields..

    #region Properties..
    #endregion Properties..

    #region Constructors..
    public CryptographyService(ILogger<CryptographyService> logger, IOptions<ApplicationConfiguration> applicationConfiguration)
    {
        _logger = logger;
        _applicationConfiguration = applicationConfiguration;
    }
    #endregion Constructors..

    #region Methods..	
    public string Encrypt(string value)
    {
        if (_applicationConfiguration.Value.Encryption_Key == null || _applicationConfiguration.Value.Encryption_IV == null)
            throw new Exception("Null encryption key/iv");

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_applicationConfiguration.Value.Encryption_Key);
            aes.IV = Encoding.UTF8.GetBytes(_applicationConfiguration.Value.Encryption_IV);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(Encoding.UTF8.GetBytes(value), 0, value.Length);
                cryptoStream.FlushFinalBlock();

                byte[] encryptedBytes = memoryStream.ToArray();
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    public string Decrypt(string encryptedValue)
    {
        if (_applicationConfiguration.Value.Encryption_Key == null || _applicationConfiguration.Value.Encryption_IV == null)
            throw new Exception("Null encryption key/iv");

        byte[] encryptedBytes = Convert.FromBase64String(encryptedValue);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_applicationConfiguration.Value.Encryption_Key);
            aes.IV = Encoding.UTF8.GetBytes(_applicationConfiguration.Value.Encryption_IV);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            using (StreamReader streamReader = new StreamReader(cryptoStream))
                return streamReader.ReadToEnd();
        }
    }
    #endregion Methods..
}
