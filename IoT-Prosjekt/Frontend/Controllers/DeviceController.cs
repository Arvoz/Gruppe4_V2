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

        public async Task<IActionResult> LightSimulator()
        {
            List<Device> devices = await _deviceService.GetDevicesAsync();

            return View(devices);
        }

        public async Task<IActionResult> CreateDevice()
        {
            return View();
        }

        public async Task<IActionResult> Create(string deviceName)
        {
            await _deviceService.CreateDevice(deviceName);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDeviceState(int id, bool state)
        {
            await _deviceService.UpdateDeviceState(id, state);
            
            return  RedirectToAction("LightSimulator");
        }

    }
}
