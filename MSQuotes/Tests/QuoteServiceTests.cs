using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Moq;
using MSQuotes.Application.Commands;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Queries;
using MSQuotes.Application.Services;
using MSQuotes.Domain;
using Xunit;

namespace MSQuotes.Tests
{
	public class QuoteServiceTests
	{
        private readonly Mock<IMediator> _mediatorMock;
        private readonly QuoteService _quoteService;

        public QuoteServiceTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _quoteService = new QuoteService(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateQuoteAsync_ShouldSendCreateQuoteCommand()
        {
            var createQuoteDto = new CreateQuoteDto
            {
                Date = DateTime.Now,
                Location = "Location A",
                PatientId = 1,
                DoctorId = 2
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateQuoteCommand>(), default))
                .ReturnsAsync(1); 

            var result = await _quoteService.CreateQuoteAsync(createQuoteDto);

            Assert.Equal(1, result);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateQuoteCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task UpdateQuoteStatusAsync_ShouldSendUpdateQuoteStatusCommand()
        {
            var updateDto = new UpdateQuoteStatusDto
            {
                Status = "Completed",
                Code = "12345",
                Description = "Test Description",
                ExpiryDate = DateTime.Now.AddDays(7)
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateQuoteStatusCommand>(), default))
               .ReturnsAsync(true);

            await _quoteService.UpdateQuoteStatusAsync(1, updateDto);

            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateQuoteStatusCommand>(), default), Times.Once);
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
                new Quote { Id = 1, Status = QuoteStatus.Pending, PatientId = 1, DoctorId = 2, Date = DateTime.Now, Location = "A" },
                new Quote { Id = 2, Status = QuoteStatus.Completed, PatientId = 3, DoctorId = 4, Date = DateTime.Now, Location = "B" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllQuotesQuery>(), default))
                .ReturnsAsync(quotes);

            var result = await _quoteService.GetAllQuotesAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Pending", result.First().Status);
            Assert.Equal("Completed", result.Last().Status);

            _mediatorMock.Verify(m => m.Send(It.IsAny<GetAllQuotesQuery>(), default), Times.Once);
        }
    }
}