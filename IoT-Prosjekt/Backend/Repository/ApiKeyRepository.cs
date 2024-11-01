using Backend.Domain;
using Backend.Ports;

namespace Backend.Repository
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly IJsonFileHandler<ApiKey> _jsonFileHandler;
        private readonly string _directoryPath = "ApiKeys";

        public ApiKeyRepository(IJsonFileHandler<ApiKey> jsonFileHandler)
        {
            _jsonFileHandler = jsonFileHandler;
        }

        public async Task DeleteApiKey(string remoteId)
        {
            var filePath = GetFilePath(remoteId);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<ApiKey> GetApiKeyByRemoteId(string remoteId)
        {
            var filePath = GetFilePath(remoteId);

            if (File.Exists(filePath))
            {
                return await _jsonFileHandler.ReadFromFile(filePath);
            }
            return null;
        }

        public async Task SaveApiKey(ApiKey apiKey)
        {
            if (!File.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            var filePath = GetFilePath(apiKey.RemoteId);
            await _jsonFileHandler.SaveToFile(apiKey, filePath);
        }

        private string GetFilePath(string remoteId)
        {
            return Path.Combine(_directoryPath, $"{remoteId}_ApiKey.json");
        }

    }
}
