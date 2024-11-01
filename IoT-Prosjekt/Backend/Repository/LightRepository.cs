using Backend.Domain;
using Backend.Ports;
using System.Reflection.Metadata;

namespace Backend.Repository
{
    public class LightRepository : ILightRepository
    {
        private readonly IJsonFileHandler<Light> _jsonFileHandler;
        private readonly string _directoryPath = "Data/Device";
        private readonly string _filePath = "lights.json";

        public LightRepository(IJsonFileHandler<Light> jsonFileHandler)
        {
            _jsonFileHandler = jsonFileHandler;

            if (!File.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
        }

        public async Task AddDevice(Light light)
        {
            var devices = await _jsonFileHandler.ReadFromFileList(_filePath);
            var maxId = devices.Any() ? devices.Max(d => d.Id) : 0;
            light.Id = maxId + 1;
            devices.Add(light);
            await _jsonFileHandler.SaveToFileList(devices, _filePath);
        }

        public async Task DeleteDevice(int id)
        {
            var devices = await _jsonFileHandler.ReadFromFileList(_filePath);
            var deviceToRemove = devices.FirstOrDefault(d => d.Id == id);
            if (deviceToRemove != null)
            {
                devices.Remove(deviceToRemove);
                await _jsonFileHandler.SaveToFileList(devices, _filePath);
            }
        }

        public async Task<List<Light>> GetAllDevices()
        {
            return await _jsonFileHandler.ReadFromFileList(_filePath);
        }

        public async Task<Light> GetDeviceById(int id)
        {
            var devices = await _jsonFileHandler.ReadFromFileList(_filePath);
            return devices.FirstOrDefault(d => d.Id == id);
        }
    }
}
