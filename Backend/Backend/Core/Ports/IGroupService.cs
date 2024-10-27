using Backend.Core.Domain;

namespace Backend.Core.Ports
{
    public interface IGroupService
    {

        Task<List<Group>> GetAllGroupsAsync();
        Task<Group> GetGroupByIdAsync(int id);
        Task<Group> GetGroupByNameAsync(string groupName);
        Task AddGroupAsync(Group group);
        Task AddDeviceToGroupAsync(int groupId, Device device);
        Task RemoveDeviceFromGroupAsync(int groupId, int deviceId);
        Task DeleteGroupAsync(int id);

    }
}
