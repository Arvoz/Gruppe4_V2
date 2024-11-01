using Backend.Domain;
using Backend.Ports;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Backend.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly ILightService _lightService;

        public GroupController(IGroupService groupService, ILightService lightService)
        {
            _groupService = groupService;
            _lightService = lightService;
        }

        [HttpPost("createGroup")]
        public async Task<IActionResult> CreateGroup(string groupName, Light light)
        {
            var device = await _lightService.GetDeviceById(light.Id);

            if (device != null)
            {
                var checkGroups = await _groupService.GetAllGroups();
                var checkGroup = checkGroups.FirstOrDefault(g => g.Name == groupName);

                if (checkGroup != null)
                {
                    await _groupService.AddDeviceToGroup(checkGroup.Id, light);
                }
                var newGroup = new Group()
                {
                    Name = groupName,
                    Devices = new List<Device> { device }
                };

                await _groupService.AddGroup(newGroup);
            }
            return Ok(device);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllGroups()
        {
            return Ok(await _groupService.GetAllGroups());
        }
        
    }
}
