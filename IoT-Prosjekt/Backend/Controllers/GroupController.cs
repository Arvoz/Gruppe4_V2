using Backend.Domain;
using Backend.Dto;
using Backend.Ports;
using Backend.Service;
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
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto request)
        {
            var device = await _lightService.GetDeviceById(request.Id);

            if (device != null)
            {
                var checkGroups = await _groupService.GetAllGroups();
                var checkGroup = checkGroups.FirstOrDefault(g => g.Name == request.GroupName);

                if (checkGroup != null)
                {
                    await _groupService.AddDeviceToGroup(checkGroup.Id, device);
                }
                else
                {    
                    var newGroup = new Group()
                    {
                        Name = request.GroupName,
                        Devices = new List<Device> { device }
                    };

                    await _groupService.AddGroup(newGroup);
                }
            }
            return Ok(device);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllGroups()
        {
            return Ok(await _groupService.GetAllGroups());
        }

        [HttpPost("deleteGroup/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _groupService.GetGroupById(id);

            if (group != null)
            {
                await _groupService.DeleteGroup(id);
                return Ok("Gruppen har blitt slettet");
            }

            return BadRequest();
        }

        [HttpPost("deleteDeviceFromGroup")]
        public async Task<IActionResult> DeleteDeviceFromGroup([FromBody] DeleteDeviceFromGroupDto request)
        {
            var group = await _groupService.GetGroupById(request.GroupId);
            if (group != null)
            {
                if (!group.Devices.Any(d => d.Id == request.DeviceId))
                {
                    return BadRequest();
                }
                await _groupService.RemoveDeviceFromGroup(group.Id, request.DeviceId);
                return Ok("Enheten har blitt slettet");
            }
            return BadRequest();
        }

        [HttpPost("updateDevices")]
        public async Task<IActionResult> UpdateDevices([FromBody] ChangeDevicesFromGroupDto request)
        {
            var groups = await _groupService.GetAllGroups();
            var group = groups.FirstOrDefault(g => g.Id == request.Id);
            if (group != null)
            {
                _lightService.UpdateLightFromGroup(group.Devices, request.State);
                _groupService.UpdateGroupDevice(group.Id, request.State);
                return Ok();
            }
            return BadRequest();
            
        }
        
    }
}
