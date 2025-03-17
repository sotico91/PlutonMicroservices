using System.Threading.Tasks;
using System.Threading;
using Moq;
using MSAuthServ.Application.Handlers;
using MSAuthServ.Application.Interfaces;
using MSAuthServ.Application.Queries;
using MSAuthServ.Domain;
using Xunit;

namespace MSAuthServ.Tests
{
	public class GetAuthByIdHandlerTests
	{
        private readonly Mock<IAuthRepository> _repositoryMock;
        private readonly GetAuthByIdHandler _handler;

        public GetAuthByIdHandlerTests()
        {
            _repositoryMock = new Mock<IAuthRepository>();
            _handler = new GetAuthByIdHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Username = "Test User" };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            var query = new GetAuthByIdQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Username, result.Username);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = 999;
            _repositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

            var query = new GetAuthByIdQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}