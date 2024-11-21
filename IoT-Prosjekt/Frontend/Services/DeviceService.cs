using Frontend.Dto;
using Frontend.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Frontend.Services
{
    public class DeviceService
    {
        private readonly HttpClient _httpClient;
        private readonly int _port= 5048;

        public DeviceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Device>> GetDevicesAsync()
        {
            var response = await _httpClient.GetAsync($"http://localhost:{_port}/api/v1/device/devices");
            response.EnsureSuccessStatusCode();
            var devices = await response.Content.ReadFromJsonAsync<List<Device>>();
            return devices;
        }

        public async Task UpdateDevicePaired(int id, bool paired)
        {
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:{_port}/api/v1/device/updatePaired/{id}", paired);

            response.EnsureSuccessStatusCode();
        }

        public async Task CreateDevice(string name)
        {
            var request = new CreateDeviceDto()
            {
                Name = name,
                Type = "light",
                State = false,
                Paired = false,
                Brightness = 100
            };

            var response = await _httpClient.PostAsJsonAsync($"http://localhost:{_port}/api/v1/device/add", request);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateDeviceState(int id, bool state)
        {
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:{_port}/api/v1/device/updateState/{id}", state);
            
            response.EnsureSuccessStatusCode();
        }

    }
}
