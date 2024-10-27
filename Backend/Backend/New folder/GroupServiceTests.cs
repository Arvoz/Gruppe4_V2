using Backend.Core.Domain;
using Backend.Core.Ports;
using Backend.Core.Service;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests.Service
{
    public class GroupServiceTests
    {
        private readonly Mock<IGroupRepository> _groupRepositoryMock;
        private readonly GroupService _groupService;

        public GroupServiceTests()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _groupService = new GroupService(_groupRepositoryMock.Object);
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
            _groupRepositoryMock.Setup(repo => repo.GetAllGroupsAsync()).ReturnsAsync(mockGroups);

            // Act
            var result = await _groupService.GetAllGroupsAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Group1", result[0].GroupName);
            Assert.Equal("Group2", result[1].GroupName);
        }

        [Fact]
        public async Task GetGroupByIdAsync_ShouldReturnGroup_WhenGroupExists()
        {
            // Arrange
            var mockGroup = new Group { Id = 1, GroupName = "Group1" };
            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync(1)).ReturnsAsync(mockGroup);

            // Act
            var result = await _groupService.GetGroupByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Group1", result.GroupName);
        }

        [Fact]
        public async Task GetGroupByIdAsync_ShouldReturnNull_WhenGroupDoesNotExist()
        {
            // Arrange
            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync(1)).ReturnsAsync((Group)null);

            // Act
            var result = await _groupService.GetGroupByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetGroupByNameAsync_ShouldReturnGroup_WhenGroupExists()
        {
            // Arrange
            var mockGroups = new List<Group>
            {
                new Group { Id = 1, GroupName = "Group1" },
                new Group { Id = 2, GroupName = "Group2" }
            };
            _groupRepositoryMock.Setup(repo => repo.GetAllGroupsAsync()).ReturnsAsync(mockGroups);

            // Act
            var result = await _groupService.GetGroupByNameAsync("Group1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Group1", result.GroupName);
        }

    }
}