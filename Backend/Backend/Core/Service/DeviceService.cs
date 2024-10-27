using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core.Domain;
using Backend.Core.Ports;

namespace Backend.Core.Service
{
    public class DeviceService : IDeviceService
    {

        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<List<Device>> GetAllDevicesAsync()
        {
            return await _deviceRepository.GetAllDevicesAsync();
        }

        public Task<Device> GetDeviceByIdAsync(int id)
        {
            return _deviceRepository.GetDeviceByIdAsync(id);
        }

        public async Task AddDeviceAsync(Device device)
        {
            await _deviceRepository.AddDeviceAsync(device);
        }

        public async Task DeleteDeviceAsync(int id)
        {
            await _deviceRepository.DeleteDeviceAsync(id);
        }

    }
}