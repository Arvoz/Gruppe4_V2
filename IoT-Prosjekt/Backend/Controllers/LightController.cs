using Backend.Domain;
using Backend.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class LightController : Controller
    {
        private readonly ILightService _lightService;

        public LightController(ILightService lightService)
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

        [HttpPost("{id}/Delete")]
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

    }
}
