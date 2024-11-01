using Backend.Core.Domain;
using Backend.Core.Ports;
using System.Runtime.CompilerServices;

namespace Backend.Infrastructure.FileStorage
{
    public class ApiRepository : IApiRepository
    {
        private readonly IJsonFileHandlerApi<Api> _jsonFileHandlerApi;
        private readonly string _directoryPath = "Apikey/";

        public ApiRepository(IJsonFileHandlerApi<Api> jsonFileHandlerApi)
        {
            _jsonFileHandlerApi = jsonFileHandlerApi;
        }

        public async Task DeleteApiAsync(string espId)
        {
            var filePath = GetFilePath(espId);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<Api> GetApiByEspIdAsync(string espId)
        {
            var filePath = GetFilePath(espId);

            if (File.Exists(filePath))
            {
                return await _jsonFileHandlerApi.ReadFromFileAsync(filePath);
            }

            return null;
        }

        public async Task SaveApiAsync(Api api)
        {
            var filePath = GetFilePath(api.EspId);
            await _jsonFileHandlerApi.SaveToFileAsync(api, filePath);
        }

        private string GetFilePath(string espId)
        {
            return Path.Combine(_directoryPath, $"{espId}_ApiKey.json");
        }

    }
}
