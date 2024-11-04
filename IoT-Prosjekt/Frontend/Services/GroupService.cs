using Frontend.Dto;
using Frontend.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Frontend.Services
{
    public class GroupService
    {
        private readonly HttpClient _httpClient;

        public GroupService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Group>> GetGroupsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7136/api/v1/group/getAll");
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
            var response = await _httpClient.PostAsJsonAsync(@"https://localhost:7136/api/v1/Group/createGroup", request);

            response.EnsureSuccessStatusCode();
        }

    }
}
