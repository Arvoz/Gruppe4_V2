using Backend.Domain;
using Backend.Ports;

namespace Backend.Service
{
    public class LightService : ILightService
    {
        private readonly ILightRepository _lightRepository;

        public LightService(ILightRepository lightRepository)
        {
            _lightRepository = lightRepository;
        }

        public async Task AddDevice(Light light)
        {
            await _lightRepository.AddDevice(light);
        }

        public async Task DeleteDevice(int id)
        {
            await _lightRepository.DeleteDevice(id);
        }

        public Task<List<Light>> GetAllDevices()
        {
            return _lightRepository.GetAllDevices();
        }

        public Task<Light> GetDeviceById(int id)
        {
            return _lightRepository.GetDeviceById(id);
        }

        public async Task UpdateDevice(int id, bool paired)
        {
            await _lightRepository.UpdateDevice(id, paired);
        }
    }
}
