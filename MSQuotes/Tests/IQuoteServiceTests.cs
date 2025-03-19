using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Moq;
using MSQuotes.Application.Commands;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Interfaces;
using MSQuotes.Application.Queries;
using MSQuotes.Application.Services;
using MSQuotes.Domain;
using Xunit;

namespace MSQuotes.Tests
{
	public class IQuoteServiceTests
	{
        private readonly Mock<IMediator> _mockMediator;
        private readonly QuoteService _quoteService;

        public IQuoteServiceTests()
        {
            _mockMediator = new Mock<IMediator>();
            _quoteService = new QuoteService(_mockMediator.Object);
        }

        [Fact]
        public async Task CreateQuoteAsync_ShouldSendCreateCommand()
        {
            var createQuoteDto = new CreateQuoteDto
            {
                Date = DateTime.Now,
                Location = "Location A",
                PatientId = 1,
                DoctorId = 2
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<CreateQuoteCommand>(), default))
                .ReturnsAsync(1);

            var result = await _quoteService.CreateQuoteAsync(createQuoteDto);

            _mockMediator.Verify(m => m.Send(It.IsAny<CreateQuoteCommand>(), default), Times.Once);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task UpdateQuoteStatusAsync_ShouldSendUpdateStatusCommand()
        {
            var updateDto = new UpdateQuoteStatusDto
            {
                Status = "Completed",
                Code = "12345",
                Description = "Test Description",
                ExpiryDate = DateTime.Now.AddDays(7)
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateQuoteStatusCommand>(), default))
                .ReturnsAsync(true);

            await _quoteService.UpdateQuoteStatusAsync(1, updateDto);

            _mockMediator.Verify(m => m.Send(It.IsAny<UpdateQuoteStatusCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task UpdateQuoteStatusAsync_ShouldThrowExceptionForInvalidStatus()
        {
            var updateDto = new UpdateQuoteStatusDto { Status = "InvalidStatus" };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
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

            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllQuotesQuery>(), default))
                .ReturnsAsync(quotes);

            var result = await _quoteService.GetAllQuotesAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Pending", result.First().Status);
            Assert.Equal("Completed", result.Last().Status);
        }
    }
}