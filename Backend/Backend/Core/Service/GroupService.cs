using Backend.Core.Domain;
using Backend.Core.Ports;

namespace Backend.Core.Service
{
    public class GroupService : IGroupService
    {

        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _groupRepository.GetAllGroupsAsync();
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {
            return await _groupRepository.GetGroupByIdAsync(id);
        }

        public async Task<Group> GetGroupByNameAsync(string groupName)
        {
            var groups = await _groupRepository.GetAllGroupsAsync();
            return groups.FirstOrDefault(g => g.GroupName == groupName);
        }

        public async Task AddGroupAsync(Group group)
        {
            await _groupRepository.AddGroupAsync(group);
        }

        public async Task AddDeviceToGroupAsync(int groupId, Device device)
        {
            await _groupRepository.AddDeviceToGroupAsync(groupId, device);
        }

        public async Task RemoveDeviceFromGroupAsync(int groupId, int deviceId)
        {
            await _groupRepository.RemoveDeviceFromGroupAsync(groupId, deviceId);
        }

        public async Task DeleteGroupAsync(int id)
        {
            await _groupRepository.DeleteGroupAsync(id);
        }

    }
}
