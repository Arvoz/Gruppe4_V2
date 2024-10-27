using Backend.Core.Domain;

namespace Backend.Core.Ports
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetAllGroupsAsync();           
        Task<Group> GetGroupByIdAsync(int id);           
        Task AddGroupAsync(Group group);                 
        Task AddDeviceToGroupAsync(int groupId, Device device); 
        Task RemoveDeviceFromGroupAsync(int groupId, int deviceId);  
        Task DeleteGroupAsync(int id);
    }
}
