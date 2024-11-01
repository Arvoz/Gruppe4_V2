using Backend.Core.Ports;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Core.Service
{
    public class EncryptionService : IEncryptionService
    {

        private readonly string _encryptionKey = "DetteErBareEnTestForSjekkOmDetFunker1234!";

        public string Decrypt(string encryptedData)
        {
            byte[] dataBytes = Convert.FromBase64String(encryptedData);
            byte[] decryptedData = ProtectedData.Unprotect(dataBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedData);
        }

        public string Encrypt(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedData = ProtectedData.Protect(dataBytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }
    }
}
