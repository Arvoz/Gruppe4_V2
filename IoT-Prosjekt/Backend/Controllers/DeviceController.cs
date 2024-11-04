using Backend.Domain;
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
        public async Task<IActionResult> Add(Light light)
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

        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpadateDeviceStatus(int id,[FromBody] bool paired)
        {
            var check = _lightService.GetDeviceById(id);
            if (check != null)
            {
                await _lightService.UpdateDevice(id, paired);
                return Ok("Alt gikk bra");
            }
            return BadRequest("Fant ikke enheten");
        }

    }
}
