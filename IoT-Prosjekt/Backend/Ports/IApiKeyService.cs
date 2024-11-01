using Backend.Domain;

namespace Backend.Ports
{
    public interface IApiKeyService
    {
        Task<ApiKey> CreateApiKey(string remoteId);
        Task DeleteApiKey(string remoteId);
        bool CheckIfExisting(string remoteId);
    }
}
