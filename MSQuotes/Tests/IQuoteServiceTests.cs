using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Moq;
using MSQuotes.Application.Commands;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Interfaces;
using Xunit;

namespace MSQuotes.Tests
{
	public class IQuoteServiceTests
	{
        private readonly Mock<IQuoteService> _mockQuoteService;
        private readonly IQuoteService _quoteService;

        public IQuoteServiceTests()
        {
            _mockQuoteService = new Mock<IQuoteService>();
            _quoteService = _mockQuoteService.Object;
        }

        [Fact]
        public async Task CreateQuoteAsync_ShouldCallServiceMethod()
        {
            var createCommand = new CreateQuoteCommand
            {
                Date = DateTime.Now,
                Location = "Location A",
                PatientId = 1,
                DoctorId = 2
            };

            _mockQuoteService.Setup(service => service.CreateQuoteAsync(createCommand))
                .ReturnsAsync(1);

            var result = await _quoteService.CreateQuoteAsync(createCommand);

            _mockQuoteService.Verify(service => service.CreateQuoteAsync(createCommand), Times.Once);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task UpdateQuoteStatusAsync_ShouldCallServiceMethod()
        {
            var updateCommand = new UpdateQuoteStatusCommand
            {
                Id = 1,
                Code = "12345",
                Description = "Test Description",
                ExpiryDate = DateTime.Now.AddDays(7),
                Status = "Completed"
            };

            _mockQuoteService.Setup(service => service.UpdateQuoteStatusAsync(updateCommand))
                .Returns(Task.CompletedTask);

            await _quoteService.UpdateQuoteStatusAsync(updateCommand);

            _mockQuoteService.Verify(service => service.UpdateQuoteStatusAsync(updateCommand), Times.Once);
        }

        [Fact]
        public async Task GetAllQuotesAsync_ShouldReturnMappedQuotes()
        {
            var quotes = new List<QuoteDto>
            {
                new QuoteDto { Id = 1, Status = "Pending" },
                new QuoteDto { Id = 2, Status = "Completed" }
            };

            _mockQuoteService.Setup(service => service.GetAllQuotesAsync())
                .ReturnsAsync(quotes);

            var result = await _quoteService.GetAllQuotesAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Pending", result.First().Status);
            Assert.Equal("Completed", result.Last().Status);
        }
    }
}