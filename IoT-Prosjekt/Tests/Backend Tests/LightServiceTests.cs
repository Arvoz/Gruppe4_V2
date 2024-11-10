using Backend.Domain;
using Backend.Ports;
using Backend.Service;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests.Service
{
    public class LightServiceTests
    {
        private readonly Mock<ILightRepository> _lightRepositoryMock;
        private readonly LightService _lightService;

        public LightServiceTests()
        {
            _lightRepositoryMock = new Mock<ILightRepository>();
            _lightService = new LightService(_lightRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllLights_ShouldReturnAllLights()
        {
            // Arrange
            var mockLights = new List<Light>
            {
                new Light { Id = 1, Name = "Light1" },
                new Light { Id = 2, Name = "Light2" }
            };
            _lightRepositoryMock.Setup(repo => repo.GetAllDevices()).ReturnsAsync(mockLights);

            // Act
            var result = await _lightService.GetAllDevices();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Light1", result[0].Name);
            Assert.Equal("Light2", result[1].Name);
        }

        [Fact]
        public async Task GetLightById_ShouldReturnLight_WhenLightExists()
        {
            // Arrange
            var mockLight = new Light { Id = 1, Name = "Light1" };
            _lightRepositoryMock.Setup(repo => repo.GetDeviceById(1)).ReturnsAsync(mockLight);

            // Act
            var result = await _lightService.GetDeviceById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Light1", result.Name);
        }

        [Fact]
        public async Task GetLightById_ShouldReturnNull_WhenLightDoesNotExist()
        {
            // Arrange
            _lightRepositoryMock.Setup(repo => repo.GetDeviceById(1)).ReturnsAsync((Light)null);

            // Act
            var result = await _lightService.GetDeviceById(1);

            // Assert
            Assert.Null(result);
        }
    }
}