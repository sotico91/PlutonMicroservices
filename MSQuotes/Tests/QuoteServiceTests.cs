using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Interfaces;
using MSQuotes.Application.Services;
using MSQuotes.Domain;
using Xunit;

namespace MSQuotes.Tests
{
	public class QuoteServiceTests
	{
        private readonly Mock<IQuoteRepository> _mockRepository;
        private readonly Mock<IRabbitMQService> _mockRabbitMQService;
        private readonly QuoteService _quoteService;


        public QuoteServiceTests()
        {
            _mockRepository = new Mock<IQuoteRepository>();
            _mockRabbitMQService = new Mock<IRabbitMQService>();
            _quoteService = new QuoteService(_mockRepository.Object, _mockRabbitMQService.Object);
        }

        [Fact]
        public async Task CreateQuoteAsync_ShouldCallAddAsyncOnce()
        {
            var createQuoteDto = new CreateQuoteDto
            {
                Date = System.DateTime.Now,
                Location = "Location A",
                PatientId = 1,
                DoctorId = 2
            };

            await _quoteService.CreateQuoteAsync(createQuoteDto);

            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Quote>()), Times.Once);
        }

        [Fact]
        public async Task UpdateQuoteStatusAsync_ShouldUpdateQuoteAndSendMessageWhenCompleted()
        {
            var quote = new Quote { Id = 1, Status = QuoteStatus.Pending, PatientId = 1 };
            var updateDto = new UpdateQuoteStatusDto
            {
                Status = "Completed",
                Code = "12345",
                Description = "Test Description",
                ExpiryDate = System.DateTime.Now.AddDays(7)
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(quote);

            await _quoteService.UpdateQuoteStatusAsync(1, updateDto);

            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Quote>()), Times.Once);
            _mockRabbitMQService.Verify(service => service.SendMessage(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UpdateQuoteStatusAsync_ShouldThrowExceptionWhenQuoteNotFound()
        {
            var updateDto = new UpdateQuoteStatusDto { Status = "Completed" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Quote)null);

            await Assert.ThrowsAsync<System.ArgumentException>(async () =>
                await _quoteService.UpdateQuoteStatusAsync(1, updateDto));
        }

        [Fact]
        public async Task GetAllQuotesAsync_ShouldReturnMappedQuotes()
        {
            var quotes = new List<Quote>
            {
                new Quote { Id = 1, Status = QuoteStatus.Pending },
                new Quote { Id = 2, Status = QuoteStatus.Completed }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(quotes);

            var result = await _quoteService.GetAllQuotesAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Pending", result[0].Status);
            Assert.Equal("Completed", result[1].Status);
        }
    }
}