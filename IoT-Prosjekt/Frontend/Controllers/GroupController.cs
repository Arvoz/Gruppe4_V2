using Frontend.Models;
using Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace Frontend.Controllers
{
    public class GroupController : Controller
    {
        private readonly GroupService _groupService;
        private readonly DeviceService _deviceService;

        public GroupController(GroupService groupService, DeviceService deviceService)
        {
            _groupService = groupService;
            _deviceService = deviceService;
        }

        public async Task<IActionResult> Index()
        {
            List<Group> groups = await _groupService.GetGroupsAsync();

            return View(groups);
        }

        public async Task<IActionResult> Create()
        {
            List<Device> devices = await _deviceService.GetDevicesAsync();
            var groups = await _groupService.GetGroupsAsync();

            ViewBag.Devices = devices;

            return View(groups);
        }

        public async Task<IActionResult> Edit(int groupId)
        {
            var groups = await _groupService.GetGroupsAsync();
            var group = groups.FirstOrDefault(g => g.Id == groupId);
            
            return View(group);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(string groupName, int deviceId)
        {
            await _groupService.CreateGroupWithDevice(groupName, deviceId);

            return RedirectToAction("Create");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDeviceFromGroup(int groupId, int deviceId)
        {
            await _groupService.RemoveDeviceFromGroup(groupId, deviceId);
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveGroup(int groupId)
        {
            await _groupService.RemoveGroup(groupId);
            
            return RedirectToAction("Index");
        }
    }
}
