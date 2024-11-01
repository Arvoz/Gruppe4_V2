using Backend.Core.Domain;

namespace Backend.Core.Ports
{
    public interface IApiService
    {
        Task<Api> CreateApiAsync(string espId);
        Task<string> GetDecryptedApiKeyAsync(string espId);
        Task DeleteApiAsync(string espId);
        bool CheckIfExisting(string espId);
    }
}
