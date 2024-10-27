using Backend.Core.Domain;
using Backend.Core.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IDeviceService _deviceService;

        public GroupController(IGroupService groupService, IDeviceService deviceService)
        {
            _groupService = groupService;
            _deviceService = deviceService;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            var devices = await _deviceService.GetAllDevicesAsync();
            ViewBag.Devices = devices;  // Legger til enhetene i ViewBag for dropdown-listen
            return View(groups);
        }

        // Metode for å opprette en ny gruppe
        [HttpPost]
        public async Task<IActionResult> AddGroup(string groupName, int deviceId)
        {
            var device = await _deviceService.GetDeviceByIdAsync(deviceId);

            if (device != null)
            {
                // Sjekk om gruppen allerede eksisterer basert på gruppenavn
                var existingGroups = await _groupService.GetAllGroupsAsync();
                var existingGroup = existingGroups.FirstOrDefault(g => g.GroupName.Equals(groupName, StringComparison.OrdinalIgnoreCase));

                if (existingGroup != null)
                {
                    // Gruppen eksisterer allerede, så vi legger til enheten i denne gruppen
                    await _groupService.AddDeviceToGroupAsync(existingGroup.Id, device);
                }
                else
                {
                    // Gruppen eksisterer ikke, så vi oppretter en ny gruppe med enheten
                    var newGroup = new Group
                    {
                        GroupName = groupName,
                        Devices = new List<Device> { device }
                    };

                    await _groupService.AddGroupAsync(newGroup);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddDeviceToGroup(int groupId, int deviceId)
        {
            var device = await _deviceService.GetDeviceByIdAsync(deviceId);
            if (device != null)
            {
                await _groupService.AddDeviceToGroupAsync(groupId, device);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _groupService.DeleteGroupAsync(id);
            return RedirectToAction("Index");
        }
    }
}
