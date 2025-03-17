using System.Threading.Tasks;
using Moq;
using MSAuthServ.Application.Interfaces;
using MSAuthServ.Domain;
using Xunit;

namespace MSAuthServ.Tests
{
	public class AuthIRepositoryTests
	{

        private readonly Mock<IAuthRepository> _repositoryMock;

        public AuthIRepositoryTests()
        {
            _repositoryMock = new Mock<IAuthRepository>();
        }

        [Fact]
        public async Task ValidateUserAsync_ShouldReturnUser_WhenCredentialsAreValid()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";
            var expectedUser = new User { Id = 1, Username = username };

            _repositoryMock
                .Setup(repo => repo.ValidateUserAsync(username, password))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _repositoryMock.Object.ValidateUserAsync(username, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Username, result.Username);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new User { Id = userId, Username = "testuser" };

            _repositoryMock
                .Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _repositoryMock.Object.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Username, result.Username);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = 999;

            _repositoryMock
                .Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync((User)null);

            // Act
            var result = await _repositoryMock.Object.GetByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }
    }
}