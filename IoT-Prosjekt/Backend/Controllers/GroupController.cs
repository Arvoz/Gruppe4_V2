﻿using Backend.Domain;
using Backend.Dto;
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
            var group = _groupService.GetGroupById(id);

            if (group != null)
            {
                await _groupService.DeleteGroup(id);
                return Ok("Gruppen har blitt slettet");
            }

            return BadRequest();
        }
        
    }
}