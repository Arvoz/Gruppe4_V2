using Frontend.Models;
using Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Frontend.Controllers
{
    public class DeviceController : Controller
    {
        private readonly DeviceService _deviceService;

        public DeviceController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public async Task<IActionResult> Index()
        {
            List<Device> devices = await _deviceService.GetDevicesAsync();

            return View(devices);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDevice(int id, bool paired)
        {
            await _deviceService.UpdateDevicePaired(id, paired);

            return RedirectToAction("Index");
        }

    }
}
