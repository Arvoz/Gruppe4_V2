using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Service;
using Backend.Core.Ports;
using Backend.Core.Domain;
using Moq;
using Xunit;

namespace Backend.Tests.Service
{
    public class DeviceServiceTests
    {
        private readonly DeviceService _deviceService;
        private readonly Mock<IDeviceRepository> _deviceRepositoryMock;

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
            _deviceRepositoryMock.Verify(repo => repo.GetAllDevicesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetDeviceByIdAsync_ShouldReturnDevice_WhenDeviceExists()
        {
            // Arrange
            var deviceId = 1;
            var mockDevice = new Device { Id = deviceId, DeviceName = "Device1" };
            _deviceRepositoryMock.Setup(repo => repo.GetDeviceByIdAsync(deviceId)).ReturnsAsync(mockDevice);

            // Act
            var result = await _deviceService.GetDeviceByIdAsync(deviceId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(deviceId, result.Id);
            _deviceRepositoryMock.Verify(repo => repo.GetDeviceByIdAsync(deviceId), Times.Once);
        }

        [Fact]
        public async Task GetDeviceByIdAsync_ShouldReturnNull_WhenDeviceDoesNotExist()
        {
            // Arrange
            var deviceId = 1;
            _deviceRepositoryMock.Setup(repo => repo.GetDeviceByIdAsync(deviceId)).ReturnsAsync((Device)null);

            // Act
            var result = await _deviceService.GetDeviceByIdAsync(deviceId);

            // Assert
            Assert.Null(result);
            _deviceRepositoryMock.Verify(repo => repo.GetDeviceByIdAsync(deviceId), Times.Once);
        }
    }
}
