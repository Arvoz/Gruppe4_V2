using Backend.Core.Domain;
using Backend.Core.Ports;
using Backend.Core.Service;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests.Service
{
    public class DeviceServiceTests
    {
        private readonly Mock<IDeviceRepository> _deviceRepositoryMock;
        private readonly DeviceService _deviceService;

        public DeviceServiceTests()
        {
            _deviceRepositoryMock = new Mock<IDeviceRepository>();
            _deviceService = new DeviceService(_deviceRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllDevicesAsync_ShouldReturnAllDevices()
        {
            // Arrange
            var mockDevices = new List<Device>
            {
                new Device { Id = 1, DeviceName = "Device1" },
                new Device { Id = 2, DeviceName = "Device2" }
            };
            _deviceRepositoryMock.Setup(repo => repo.GetAllDevicesAsync()).ReturnsAsync(mockDevices);

            // Act
            var result = await _deviceService.GetAllDevicesAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Device1", result[0].DeviceName);
            Assert.Equal("Device2", result[1].DeviceName);
        }

        [Fact]
        public async Task GetDeviceByIdAsync_ShouldReturnDevice_WhenDeviceExists()
        {
            // Arrange
            var mockDevice = new Device { Id = 1, DeviceName = "Device1" };
            _deviceRepositoryMock.Setup(repo => repo.GetDeviceByIdAsync(1)).ReturnsAsync(mockDevice);

            // Act
            var result = await _deviceService.GetDeviceByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Device1", result.DeviceName);
        }

        [Fact]
        public async Task GetDeviceByIdAsync_ShouldReturnNull_WhenDeviceDoesNotExist()
        {
            // Arrange
            _deviceRepositoryMock.Setup(repo => repo.GetDeviceByIdAsync(1)).ReturnsAsync((Device)null);

            // Act
            var result = await _deviceService.GetDeviceByIdAsync(1);

            // Assert
            Assert.Null(result);
        }
    }
}