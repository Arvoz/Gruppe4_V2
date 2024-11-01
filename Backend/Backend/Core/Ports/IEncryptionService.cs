namespace Backend.Core.Ports
{
    public interface IEncryptionService
    {
        string Encrypt(string data);
        string Decrypt(string encryptedData);
    }
}
