using Backend.Domain;
using Backend.Ports;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata;

namespace Backend.Repository
{
    public class LightRepository : ILightRepository
    {
        private readonly IJsonFileHandler<Light> _jsonFileHandler;
        private readonly string _directoryPath = "Data/Device";

        public LightRepository(IJsonFileHandler<Light> jsonFileHandler)
        {
            _jsonFileHandler = jsonFileHandler;
        }

        public async Task AddDevice(Light light)
        {
            var filePath = GetFilePath();
            if (!File.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            var devices = await _jsonFileHandler.ReadFromFileList(filePath);
            var maxId = devices.Any() ? devices.Max(d => d.Id) : 0;
            light.Id = maxId + 1;
            devices.Add(light);
            await _jsonFileHandler.SaveToFileList(devices, filePath);
        }

        public async Task DeleteDevice(int id)
        {
            var filePath = GetFilePath();
            var devices = await _jsonFileHandler.ReadFromFileList(filePath);
            var deviceToRemove = devices.FirstOrDefault(d => d.Id == id);
            if (deviceToRemove != null)
            {
                devices.Remove(deviceToRemove);
                await _jsonFileHandler.SaveToFileList(devices, filePath);
            }
        }

        public async Task<List<Light>> GetAllDevices()
        {
            var filePath = GetFilePath();
            return await _jsonFileHandler.ReadFromFileList(filePath);
        }

        public async Task<Light> GetDeviceById(int id)
        {
            var filePath = GetFilePath();
            var devices = await _jsonFileHandler.ReadFromFileList(filePath);
            return devices.FirstOrDefault(d => d.Id == id);
        }

        public async Task UpdateDevicePaired(int id, bool state)
        {
            var filePath = GetFilePath();
            var devices = await _jsonFileHandler.ReadFromFileList(filePath);
            var device = devices.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                device.ChangePaired(state);
                await _jsonFileHandler.SaveToFileList(devices, filePath);
                Console.WriteLine(device.Paired);
            }
        }

        public async Task UpdateDevicesFromGroup(int id, bool paired)
        {
            var filePath = GetFilePath();
            var devices = await _jsonFileHandler.ReadFromFileList(filePath);
            var device = devices.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                device.ChangeOnOrOff(paired);
                await _jsonFileHandler.SaveToFileList(devices, filePath);
            }
        }

        public async Task UpdateDeviceState(int id, bool state)
        {
            var filePath = GetFilePath();
            var devices = await _jsonFileHandler.ReadFromFileList(filePath);
            var device = devices.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                device.ChangeOnOrOff(state);
                await _jsonFileHandler.SaveToFileList(devices, filePath);
            }
        }

        private string GetFilePath()
        {
            return Path.Combine(_directoryPath, $"device.json");
        }
    }
}
