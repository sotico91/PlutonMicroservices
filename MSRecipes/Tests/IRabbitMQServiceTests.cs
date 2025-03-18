using System.Threading.Tasks;
using Moq;
using MSRecipes.Application.Interfaces;
using Xunit;

namespace MSRecipes.Tests
{
	public class IRabbitMQServiceTests
    {
        private readonly Mock<IRabbitMQService> _rabbitMQServiceMock;

        public IRabbitMQServiceTests()
        {
            _rabbitMQServiceMock = new Mock<IRabbitMQService>();
        }

        [Fact]
        public void StartListening_ShouldBeCalled()
        {
            _rabbitMQServiceMock.Object.StartListening();
            _rabbitMQServiceMock.Verify(service => service.StartListening(), Times.Once);
        }

        [Fact]
        public async Task HandleMessageAsync_ShouldBeCalledWithCorrectMessage()
        {
            var message = "Sample message";
            await _rabbitMQServiceMock.Object.HandleMessageAsync(message);

            _rabbitMQServiceMock.Verify(service => service.HandleMessageAsync(message), Times.Once);
        }
    }
}