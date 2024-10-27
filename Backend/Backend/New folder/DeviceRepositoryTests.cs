using Backend.Core.Domain;
using Backend.Infrastructure.FileStorage;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests.FileStorage
{
    public class DeviceRepositoryTests
    {
        private readonly Mock<IJsonFileHandler<Device>> _jsonFileHandlerMock;
        private readonly DeviceRepository _deviceRepository;

        public DeviceRepositoryTests()
        {
            _jsonFileHandlerMock = new Mock<IJsonFileHandler<Device>>();
            _deviceRepository = new DeviceRepository(_jsonFileHandlerMock.Object);
        }

        [Fact]
        public async Task GetAllDevicesAsync_ShouldReturnDevices()
        {
            // Arrange
            var mockDevices = new List<Device>
            {
                new Device { Id = 1, DeviceName = "Device1" },
                new Device { Id = 2, DeviceName = "Device2" }
            };
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(mockDevices);

            // Act
            var result = await _deviceRepository.GetAllDevicesAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Device1", result[0].DeviceName);
            Assert.Equal("Device2", result[1].DeviceName);
        }
        [Fact]
        public async Task GetDeviceByIdAsync_ShouldReturnDevice_WhenDeviceExists()
        {
            // Arrange
            var mockDevices = new List<Device>
            {
                new Device { Id = 1, DeviceName = "Device1" },
                new Device { Id = 2, DeviceName = "Device2" }
            };
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(mockDevices);

            // Act
            var result = await _deviceRepository.GetDeviceByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Device1", result.DeviceName);
        }

        [Fact]
        public async Task GetDeviceByIdAsync_ShouldReturnNull_WhenDeviceDoesNotExist()
        {
            // Arrange
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(new List<Device>());

            // Act
            var result = await _deviceRepository.GetDeviceByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddDeviceAsync_ShouldAddDeviceWithIncrementedId()
        {
            // Arrange
            var mockDevices = new List<Device>
            {
                new Device { Id = 1, DeviceName = "Device1" }
            };
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(mockDevices);

            var newDevice = new Device { DeviceName = "Device2" };

            // Act
            await _deviceRepository.AddDeviceAsync(newDevice);

            // Assert
            Assert.Equal(2, newDevice.Id); 
            _jsonFileHandlerMock.Verify(handler => handler.SaveToFileAsync(It.Is<List<Device>>(d => d.Count == 2)), Times.Once);
        }

        [Fact]
        public async Task DeleteDeviceAsync_ShouldRemoveDevice_WhenDeviceExists()
        {
            // Arrange
            var mockDevices = new List<Device>
            {
                new Device { Id = 1, DeviceName = "Device1" },
                new Device { Id = 2, DeviceName = "Device2" }
            };
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(mockDevices);

            // Act
            await _deviceRepository.DeleteDeviceAsync(1);

            // Assert
            _jsonFileHandlerMock.Verify(handler => handler.SaveToFileAsync(It.Is<List<Device>>(d => d.Count == 1 && d.All(dev => dev.Id == 2))), Times.Once);
        }

        [Fact]
        public async Task DeleteDeviceAsync_ShouldNotModifyList_WhenDeviceDoesNotExist()
        {
            // Arrange
            var mockDevices = new List<Device>
            {
                new Device { Id = 1, DeviceName = "Device1" },
                new Device { Id = 2, DeviceName = "Device2" }
            };
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(mockDevices);

            // Act
            await _deviceRepository.DeleteDeviceAsync(3);

            // Assert
            _jsonFileHandlerMock.Verify(handler => handler.SaveToFileAsync(It.IsAny<List<Device>>()), Times.Never);
        }
    }
}