using Backend.Core.Domain;

namespace Backend.Core.Ports
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAllDevicesAsync();
        Task<Device> GetDeviceByIdAsync(int id);
        Task AddDeviceAsync(Device device);
        Task DeleteDeviceAsync(int id);
    }
}