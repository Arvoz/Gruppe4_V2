using Backend.Core.Domain;
using System.Numerics;

namespace Backend.Core.Ports
{
    public interface IApiRepository
    {
        Task<Api> GetApiByEspIdAsync(string espId);
        Task SaveApiAsync(Api api);
        Task DeleteApiAsync(string espId);

    }
}
