using Backend.Domain;
using Backend.Ports;

namespace Backend.Service
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task AddDeviceToGroup(int groupId, Device device)
        {
            await _groupRepository.AddDeviceToGroup(groupId, device);
        }

        public async Task AddGroup(Group group)
        {
            await _groupRepository.AddGroup(group);
        }

        public async Task DeleteGroup(int groupId)
        {
            await _groupRepository.DeleteGroup(groupId);
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _groupRepository.GetAllGroups();
        }

        public Task<Group> GetGroupById(int groupId)
        {
            return _groupRepository.GetGroupById(groupId);
        }

        public async Task<Group> GetGroupByName(string groupName)
        {
            var group = await _groupRepository.GetAllGroups();
            return group.FirstOrDefault(g => g.Name == groupName);
        }

        public async Task RemoveDeviceFromGroup(int groupId, int deviceId)
        {
            await _groupRepository.RemoveDeviceFromGroup(groupId, deviceId);
        }
    }
}
