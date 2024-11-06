using Frontend.Dto;
using Frontend.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Frontend.Services
{
    public class GroupService
    {
        private readonly HttpClient _httpClient;
        private readonly int _port = 5048;

        public GroupService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Group>> GetGroupsAsync()
        {
            var response = await _httpClient.GetAsync($"http://localhost:{_port}/api/v1/group/getAll");
            response.EnsureSuccessStatusCode();
            var groups = await response.Content.ReadFromJsonAsync<List<Group>>();
            return groups;
        }

        public async Task CreateGroupWithDevice(string groupName, int deviceId)
        {
            var request = new CreateGroupDto()
            {
                GroupName = groupName,
                Id = deviceId
            };
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:{_port}/api/v1/Group/createGroup", request);

            response.EnsureSuccessStatusCode();
        }

    }
}
