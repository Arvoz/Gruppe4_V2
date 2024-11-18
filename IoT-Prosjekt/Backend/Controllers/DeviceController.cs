using Backend.Domain;
using Backend.Dto;
using Backend.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class DeviceController : Controller
    {
        private readonly ILightService _lightService;

        public DeviceController(ILightService lightService)
        {
            _lightService = lightService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Light light)
        {
            await _lightService.AddDevice(light);
            return Ok(light);
        }

        [HttpGet("devices")]
        public async Task<IActionResult> GetAllDevices()
        {
            return Ok(await _lightService.GetAllDevices());
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var devices = await _lightService.GetAllDevices();
            var device = devices.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                await _lightService.DeleteDevice(id);
                return Ok(device);
            }
            return BadRequest("Enheten finnes ikke");
        }

        [HttpPost("updatePaired/{id}")]
        public async Task<IActionResult> UpdateDevicePaired(int id, [FromBody] bool paired)
        {
            var check = await _lightService.GetDeviceById(id);
            if (check != null)
            {
                await _lightService.UpdateDevicePaired(id, paired);
                return Ok("Alt gikk bra");
            }
            return BadRequest("Fant ikke enheten");
        }

        [HttpPost("updateState/{id}")]
        public async Task<IActionResult> UpdateDeviceState(int id, [FromBody] bool state)
        {
            var check = await _lightService.GetDeviceById(id);
            if (check != null)
            {
                await _lightService.UpdateDeviceState(id, state);
                return Ok();
            }
            return BadRequest();
        }
        

    }
}
