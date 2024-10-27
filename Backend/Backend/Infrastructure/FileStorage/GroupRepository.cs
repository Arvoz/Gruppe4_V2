using Backend.Core.Domain;
using Backend.Core.Ports;

namespace Backend.Infrastructure.FileStorage
{
    public class GroupRepository : IGroupRepository
    {

        private readonly JsonFileHandler<Group> _jsonFileHandler;

        public GroupRepository(string filePath)
        {
            _jsonFileHandler = new JsonFileHandler<Group>(filePath);
        }

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _jsonFileHandler.ReadFromFileAsync();
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {
            var groups = await _jsonFileHandler.ReadFromFileAsync();
            return groups.FirstOrDefault(g => g.Id == id);
        }

        public async Task AddGroupAsync(Group group)
        {
            var groups = await _jsonFileHandler.ReadFromFileAsync();
            var maxId = groups.Any() ? groups.Max(g => g.Id) : 0;
            group.Id = maxId + 1;
            groups.Add(group);
            await _jsonFileHandler.SaveToFileAsync(groups);
        }

        public async Task AddDeviceToGroupAsync(int groupId, Device device)
        {
            var groups = await _jsonFileHandler.ReadFromFileAsync();
            var group = groups.FirstOrDefault(g => g.Id == groupId);
            if (group != null)
            {
                group.Devices.Add(device);
                await _jsonFileHandler.SaveToFileAsync(groups);
            }
        }

        public async Task RemoveDeviceFromGroupAsync(int groupId, int deviceId)
        {
            var groups = await _jsonFileHandler.ReadFromFileAsync();
            var group = groups.FirstOrDefault(g => g.Id == groupId);
            if (group != null)
            {
                var deviceToRemove = group.Devices.FirstOrDefault(g => g.Id == deviceId);
                if (deviceToRemove != null)
                {
                    group.Devices.Remove(deviceToRemove);
                    await _jsonFileHandler.SaveToFileAsync(groups);
                }
            }
        }

        public async Task DeleteGroupAsync(int id)
        {
            var groups = await _jsonFileHandler.ReadFromFileAsync();
            var groupToRemove = groups.FirstOrDefault(g => g.Id == id);
            if (groupToRemove != null)
            {
                groups.Remove(groupToRemove);
                await _jsonFileHandler.SaveToFileAsync(groups);
            }
        }

    }
}
