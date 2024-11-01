using Backend.Domain;
using Backend.Ports;

namespace Backend.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IJsonFileHandler<Group> _jsonFileHandler;
        private readonly string _filePath = "groups.json";

        public GroupRepository(IJsonFileHandler<Group> jsonFileHandler)
        {
            _jsonFileHandler = jsonFileHandler;
        }

        public async Task AddDeviceToGroup(int groupId, Device device)
        {
            var groups = await _jsonFileHandler.ReadFromFileList(_filePath);
            var group = groups.FirstOrDefault(g => g.Id == groupId);
            if (group != null)
            {
                group.Devices.Add(device);
                await _jsonFileHandler.SaveToFileList(groups, _filePath);
            }
        }

        public async Task AddGroup(Group group)
        {
            var groups = await _jsonFileHandler.ReadFromFileList(_filePath);
            var maxId = groups.Any() ? groups.Max(g => g.Id) : 0;
            group.Id = maxId + 1;
            groups.Add(group);
            await _jsonFileHandler.SaveToFileList(groups, _filePath);
        }

        public async Task DeleteGroup(int groupId)
        {
            var groups = await _jsonFileHandler.ReadFromFileList(_filePath);
            var groupToRemove = groups.FirstOrDefault(g => g.Id == groupId);
            if (groupToRemove != null)
            {
                groups.Remove(groupToRemove);
                await _jsonFileHandler.SaveToFileList(groups, _filePath);
            }
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _jsonFileHandler.ReadFromFileList(_filePath);
        }

        public async Task<Group> GetGroupById(int id)
        {
            var groups = await _jsonFileHandler.ReadFromFileList(_filePath);
            return groups.FirstOrDefault(g => g.Id == id);
        }

        public async Task RemoveDeviceFromGroup(int groupId, int deviceId)
        {
            var groups = await _jsonFileHandler.ReadFromFileList(_filePath);
            var group = groups.FirstOrDefault(g => g.Id == groupId);
            if (group != null)
            {
                var deviceToRemove = group.Devices.FirstOrDefault(d => d.Id == deviceId);
                if (deviceToRemove != null)
                {
                    group.Devices.Remove(deviceToRemove);
                    await _jsonFileHandler.SaveToFileList(groups, _filePath);
                }
            }
        }

    }
}
