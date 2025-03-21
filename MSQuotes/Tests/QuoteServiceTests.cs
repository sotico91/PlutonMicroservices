using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MSQuotes.Application.Commands;
using MSQuotes.Application.Interfaces;
using MSQuotes.Application.Services;
using MSQuotes.Domain;
using Xunit;

namespace MSQuotes.Tests
{
	public class QuoteServiceTests
	{
        private readonly Mock<IQuoteRepository> _quoteRepositoryMock;
        private readonly Mock<IRabbitMQService> _rabbitMQServiceMock;
        private readonly QuoteService _quoteService;

        public QuoteServiceTests()
        {
            _quoteRepositoryMock = new Mock<IQuoteRepository>();
            _rabbitMQServiceMock = new Mock<IRabbitMQService>();
            _quoteService = new QuoteService(_quoteRepositoryMock.Object, _rabbitMQServiceMock.Object);
        }

        [Fact]
        public async Task CreateQuoteAsync_ShouldAddQuoteAndReturnId()
        {
            var createQuoteCommand = new CreateQuoteCommand
            {
                Date = DateTime.Now,
                Location = "Location A",
                PatientId = 1,
                DoctorId = 2
            };

            var quote = new Quote
            {
                Id = 1,
                Date = createQuoteCommand.Date,
                Location = createQuoteCommand.Location,
                PatientId = createQuoteCommand.PatientId,
                DoctorId = createQuoteCommand.DoctorId,
                Status = QuoteStatus.Pending
            };

            _quoteRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Quote>()))
                .Callback<Quote>(q => q.Id = 1)
                .Returns(Task.CompletedTask);

            var result = await _quoteService.CreateQuoteAsync(createQuoteCommand);

            Assert.Equal(1, result);
            _quoteRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Quote>()), Times.Once);
        }

        [Fact]
        public async Task UpdateQuoteStatusAsync_ShouldUpdateQuoteAndSendMessageIfCompleted()
        {
            var updateQuoteCommand = new UpdateQuoteStatusCommand
            {
                Id = 1,
                Status = "Completed",
                Code = "12345",
                Description = "Test Description",
                ExpiryDate = DateTime.Now.AddDays(7)
            };

            var existingQuote = new Quote { Id = 1, Status = QuoteStatus.Pending, PatientId = 1 };

            _quoteRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingQuote);
            _quoteRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Quote>())).Returns(Task.CompletedTask);
            _rabbitMQServiceMock.Setup(r => r.SendMessage(It.IsAny<string>())).Verifiable();

            await _quoteService.UpdateQuoteStatusAsync(updateQuoteCommand);

            _quoteRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Quote>()), Times.Once);
            _rabbitMQServiceMock.Verify(r => r.SendMessage(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UpdateQuoteStatusAsync_ShouldThrowExceptionForInvalidStatus()
        {
            var updateQuoteCommand = new UpdateQuoteStatusCommand { Id = 1, Status = "InvalidStatus" };
            var existingQuote = new Quote { Id = 1, Status = QuoteStatus.Pending };

            _quoteRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingQuote);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _quoteService.UpdateQuoteStatusAsync(updateQuoteCommand));
        }

        [Fact]
        public async Task GetAllQuotesAsync_ShouldReturnMappedQuotes()
        {
            var quotes = new List<Quote>
            {
                new Quote { Id = 1, Status = QuoteStatus.Pending, PatientId = 1, DoctorId = 2, Date = DateTime.Now, Location = "A" },
                new Quote { Id = 2, Status = QuoteStatus.Completed, PatientId = 3, DoctorId = 4, Date = DateTime.Now, Location = "B" }
            };

            _quoteRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(quotes);

            var result = await _quoteService.GetAllQuotesAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Pending", result.First().Status);
            Assert.Equal("Completed", result.Last().Status);

            _quoteRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}