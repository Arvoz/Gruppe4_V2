using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core.Domain;
using Backend.Core.Ports;
using Backend.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public async Task<IActionResult> Index()
        {
            var devices = await _deviceService.GetAllDevicesAsync();
            return View(devices);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Device device)
        {
            await _deviceService.AddDeviceAsync(device);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _deviceService.DeleteDeviceAsync(id);
            return RedirectToAction("Index");
        }

    }
}