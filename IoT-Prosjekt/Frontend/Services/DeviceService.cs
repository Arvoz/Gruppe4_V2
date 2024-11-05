using Backend.Dto;
using Frontend.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Frontend.Services
{
    public class DeviceService
    {
        private readonly HttpClient _httpClient;

        public DeviceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Device>> GetDevicesAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7136/api/v1/device/devices");
            response.EnsureSuccessStatusCode();
            var devices = await response.Content.ReadFromJsonAsync<List<Device>>();
            return devices;
        }

        public async Task UpdateDevicePaired(int id, bool paired)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7136/api/v1/device/updatePaired/{id}", paired);

            response.EnsureSuccessStatusCode();
        }

        public async Task CreateDevice(string name)
        {
            var request = new CreateDeviceDto()
            {
                Name = name,
                State = false,
                Paired = false,
                Brightness = 100
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7136/api/v1/device/add", request);

            response.EnsureSuccessStatusCode();
        }

    }
}
