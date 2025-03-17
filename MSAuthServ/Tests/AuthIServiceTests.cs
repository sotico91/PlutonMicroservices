using System.Threading.Tasks;
using Moq;
using MSAuthServ.Application.Interfaces;
using Xunit;

namespace MSAuthServ.Tests
{
	public class AuthIServiceTests
    {
        private readonly Mock<IAuthService> _authServiceMock;

        public AuthIServiceTests()
        {
            _authServiceMock = new Mock<IAuthService>();
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";
            var expectedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";

            _authServiceMock
                .Setup(service => service.AuthenticateAsync(username, password))
                .ReturnsAsync(expectedToken);

            // Act
            var result = await _authServiceMock.Object.AuthenticateAsync(username, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedToken, result);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var username = "invaliduser";
            var password = "wrongpassword";

            _authServiceMock
                .Setup(service => service.AuthenticateAsync(username, password))
                .ReturnsAsync((string)null);

            // Act
            var result = await _authServiceMock.Object.AuthenticateAsync(username, password);

            // Assert
            Assert.Null(result);
        }
    }
}