using Backend.Core.Domain;
using Backend.Infrastructure.FileStorage;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests.FileStorage
{
    public class GroupRepositoryTests
    {
        private readonly Mock<IJsonFileHandler<Group>> _jsonFileHandlerMock;
        private readonly GroupRepository _groupRepository;

        public GroupRepositoryTests()
        {
            _jsonFileHandlerMock = new Mock<IJsonFileHandler<Group>>();
            _groupRepository = new GroupRepository(_jsonFileHandlerMock.Object);
        }

        [Fact]
        public async Task GetAllGroupsAsync_ShouldReturnAllGroups()
        {
            // Arrange
            var mockGroups = new List<Group>
            {
                new Group { Id = 1, GroupName = "Group1" },
                new Group { Id = 2, GroupName = "Group2" }
            };
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(mockGroups);

            // Act
            var result = await _groupRepository.GetAllGroupsAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Group1", result[0].GroupName);
            Assert.Equal("Group2", result[1].GroupName);
        }

        [Fact]
        public async Task GetGroupByIdAsync_ShouldReturnGroup_WhenGroupExists()
        {
            // Arrange
            var mockGroups = new List<Group>
            {
                new Group { Id = 1, GroupName = "Group1" },
                new Group { Id = 2, GroupName = "Group2" }
            };
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(mockGroups);

            // Act
            var result = await _groupRepository.GetGroupByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Group1", result.GroupName);
        }

        [Fact]
        public async Task GetGroupByIdAsync_ShouldReturnNull_WhenGroupDoesNotExist()
        {
            // Arrange
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(new List<Group>());

            // Act
            var result = await _groupRepository.GetGroupByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddGroupAsync_ShouldAddGroupWithIncrementedId()
        {
            // Arrange
            var mockGroups = new List<Group>
            {
                new Group { Id = 1, GroupName = "Group1" }
            };
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(mockGroups);

            var newGroup = new Group { GroupName = "Group2" };

            // Act
            await _groupRepository.AddGroupAsync(newGroup);

            // Assert
            Assert.Equal(2, newGroup.Id); 
            _jsonFileHandlerMock.Verify(handler => handler.SaveToFileAsync(It.Is<List<Group>>(g => g.Count == 2)), Times.Once);
        }

        [Fact]
        public async Task AddDeviceToGroupAsync_ShouldAddDeviceToGroup_WhenGroupExists()
        {
            // Arrange
            var mockGroups = new List<Group>
            {
                new Group { Id = 1, GroupName = "Group1", Devices = new List<Device>() }
            };
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(mockGroups);

            var newDevice = new Device { Id = 1, DeviceName = "Device1" };

            // Act
            await _groupRepository.AddDeviceToGroupAsync(1, newDevice);

            // Assert
            _jsonFileHandlerMock.Verify(handler => handler.SaveToFileAsync(It.Is<List<Group>>(g => g.First().Devices.Count == 1)), Times.Once);
        }

        [Fact]
        public async Task RemoveDeviceFromGroupAsync_ShouldRemoveDeviceFromGroup_WhenDeviceExists()
        {
            // Arrange
            var mockGroups = new List<Group>
            {
                new Group { Id = 1, GroupName = "Group1", Devices = new List<Device> { new Device { Id = 1, DeviceName = "Device1" } } }
            };
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(mockGroups);

            // Act
            await _groupRepository.RemoveDeviceFromGroupAsync(1, 1);

            // Assert
            _jsonFileHandlerMock.Verify(handler => handler.SaveToFileAsync(It.Is<List<Group>>(g => g.First().Devices.Count == 0)), Times.Once);
        }

        [Fact]
        public async Task DeleteGroupAsync_ShouldRemoveGroup_WhenGroupExists()
        {
            // Arrange
            var mockGroups = new List<Group>
            {
                new Group { Id = 1, GroupName = "Group1" },
                new Group { Id = 2, GroupName = "Group2" }
            };
            _jsonFileHandlerMock.Setup(handler => handler.ReadFromFileAsync()).ReturnsAsync(mockGroups);

            // Act
            await _groupRepository.DeleteGroupAsync(1);

            // Assert
            _jsonFileHandlerMock.Verify(handler => handler.SaveToFileAsync(It.Is<List<Group>>(g => g.Count == 1 && g.First().Id == 2)), Times.Once);
        }
    }
}