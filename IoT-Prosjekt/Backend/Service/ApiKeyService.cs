using Backend.Domain;
using Backend.Ports;

namespace Backend.Service
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyService(IApiKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
        }

        public bool CheckIfExisting(string remoteId)
        {
            var check = _apiKeyRepository.GetApiKeyByRemoteId(remoteId);
            if (check != null)
            {
                return true;
            }
            return false;
        }

        public async Task<ApiKey> CreateApiKey(string remoteId)
        {
            string apiKey = Guid.NewGuid().ToString();
            
            var api = new ApiKey()
            {
                RemoteId = remoteId,
                Key = apiKey
            };

            await _apiKeyRepository.SaveApiKey(api);
            return api;
        }

        public async Task DeleteApiKey(string remoteId)
        {
            await _apiKeyRepository.DeleteApiKey(remoteId);
        }
    }
}
