using System.Threading.Tasks;
using Moq;
using MSAuthServ.Application.Interfaces;
using MSAuthServ.Application.Services;
using MSAuthServ.Domain;
using Xunit;

namespace MSAuthServ.Tests
{
	public class AuthServiceTests
	{
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly AuthService _authService;
        private const string Issuer = "TestIssuer";
        private const string Audience = "TestAudience";
        private const string SecretKey = "SuperSecretKey123456789";

        public AuthServiceTests()
        {
            _authRepositoryMock = new Mock<IAuthRepository>();
            _authService = new AuthService(_authRepositoryMock.Object, Issuer, Audience, SecretKey);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var username = "testUser";
            var password = "password";
            var user = new User { Id = 1, Username = username };

            _authRepositoryMock.Setup(repo => repo.ValidateUserAsync(username, password))
                               .ReturnsAsync(user);

            // Act
            var token = await _authService.AuthenticateAsync(username, password);

            // Assert
            Assert.NotNull(token);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var username = "invalidUser";
            var password = "wrongPassword";

            _authRepositoryMock.Setup(repo => repo.ValidateUserAsync(username, password))
                               .ReturnsAsync((User)null);

            // Act
            var token = await _authService.AuthenticateAsync(username, password);

            // Assert
            Assert.Null(token);
        }
    }
}