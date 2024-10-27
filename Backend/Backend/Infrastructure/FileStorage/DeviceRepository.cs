using Backend.Core.Domain;
using Backend.Core.Ports;

namespace Backend.Infrastructure.FileStorage
{
    public class DeviceRepository : IDeviceRepository
{
    private readonly IJsonFileHandler<Device> _jsonFileHandler;

    public DeviceRepository(IJsonFileHandler<Device> jsonFileHandler)
    {
        _jsonFileHandler = jsonFileHandler;
    }

    public async Task<List<Device>> GetAllDevicesAsync()
    {
        return await _jsonFileHandler.ReadFromFileAsync();
    }

    public async Task<Device> GetDeviceByIdAsync(int id)
    {
        var devices = await _jsonFileHandler.ReadFromFileAsync();
        return devices.FirstOrDefault(d => d.Id == id);
    }

    public async Task AddDeviceAsync(Device device)
    {
        var devices = await _jsonFileHandler.ReadFromFileAsync();
        var maxId = devices.Any() ? devices.Max(d => d.Id) : 0;
        device.Id = maxId + 1;
        devices.Add(device);
        await _jsonFileHandler.SaveToFileAsync(devices);
    }

    public async Task DeleteDeviceAsync(int id)
    {
        var devices = await _jsonFileHandler.ReadFromFileAsync();
        var deviceToRemove = devices.FirstOrDefault(d => d.Id == id);
        if (deviceToRemove != null)
        {
            devices.Remove(deviceToRemove);
            await _jsonFileHandler.SaveToFileAsync(devices);
        }
    }
}
}
