using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Interfaces;
using Xunit;

namespace MSQuotes.Tests
{
	public class IQuoteServiceTests
	{
        private readonly Mock<IQuoteService> _mockService;

        public IQuoteServiceTests()
        {
            _mockService = new Mock<IQuoteService>();
        }

        [Fact]
        public async Task CreateQuoteAsync_ShouldCallCreateQuoteOnce()
        {
            var createQuoteDto = new CreateQuoteDto { DoctorId = 1, PatientId = 2, Location = "Clinic A" };

            await _mockService.Object.CreateQuoteAsync(createQuoteDto);

            _mockService.Verify(service => service.CreateQuoteAsync(createQuoteDto), Times.Once);
        }

        [Fact]
        public async Task UpdateQuoteStatusAsync_ShouldCallUpdateQuoteStatusOnce()
        {
            var updateQuoteStatusDto = new UpdateQuoteStatusDto { Status = "Approved" };

            await _mockService.Object.UpdateQuoteStatusAsync(1, updateQuoteStatusDto);

            _mockService.Verify(service => service.UpdateQuoteStatusAsync(1, updateQuoteStatusDto), Times.Once);
        }

        [Fact]
        public async Task GetAllQuotesAsync_ShouldReturnQuotes()
        {
            var quotes = new List<QuoteDto>
            {
                new QuoteDto { DoctorId = 1, PatientId = 2, Location = "Clinic A"},
                new QuoteDto {DoctorId = 1, PatientId = 3, Location = "Clinic A" }
            };

            _mockService.Setup(service => service.GetAllQuotesAsync()).ReturnsAsync(quotes);

            var result = await _mockService.Object.GetAllQuotesAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}