using Backend.Core.Domain;
using Backend.Core.Ports;

namespace Backend.Core.Service
{
    public class ApiService : IApiService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly IApiRepository _apiRepository;

        public ApiService(IEncryptionService encryptionService, IApiRepository apiRepository)
        {
            _encryptionService = encryptionService;
            _apiRepository = apiRepository;
        }

        public bool CheckIfExisting(string espId)
        {
            var check = _apiRepository.GetApiByEspIdAsync(espId);
            if (check == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Api> CreateApiAsync(string espId)
        {
            string apiKey = Guid.NewGuid().ToString();
            string encryptedApiKey = _encryptionService.Encrypt(apiKey);

            var api = new Api()
            {
                EspId = espId,
                ApiKey = encryptedApiKey
            };

            await _apiRepository.SaveApiAsync(api);
            return api;

        }

        public async Task DeleteApiAsync(string espId)
        {
            await _apiRepository.DeleteApiAsync(espId);
        }

        public async Task<string> GetDecryptedApiKeyAsync(string espId)
        {
            var api = await _apiRepository.GetApiByEspIdAsync(espId);
            if (api == null)
            {
                return null;
            }
            var apiDecrypted = _encryptionService.Decrypt(api.ApiKey);
            return apiDecrypted;
            
        }

    }
}
