using System;
using System.Threading.Tasks;
using System.Threading;
using Moq;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Handlers;
using MSQuotes.Application.Interfaces;
using MSQuotes.Application.Queries;
using Xunit;

namespace MSQuotes.Tests
{
	public class GetQuoteByIdHandlerTests
	{
        private readonly Mock<IQuoteRepository> _quoteRepositoryMock;
        private readonly GetQuoteByIdHandler _handler;

        public GetQuoteByIdHandlerTests()
        {
            _quoteRepositoryMock = new Mock<IQuoteRepository>();
            _handler = new GetQuoteByIdHandler(_quoteRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnQuoteDto_WhenQuoteExists()
        {
            var query = new GetQuoteByIdQuery(999);
            var quote = new MSQuotes.Domain.Quote
            {
                Id = 999,
                Date = DateTime.Now,
                Location = "Clinic A",
                PatientId = 101,
                DoctorId = 202,
                Status = MSQuotes.Domain.QuoteStatus.Pending
            };

            _quoteRepositoryMock.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync(quote);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(999, result.Id);
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentException_WhenQuoteNotFound()
        {
            var query = new GetQuoteByIdQuery(999);
            _quoteRepositoryMock.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((MSQuotes.Domain.Quote)null);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(query, CancellationToken.None));
        }
    }
}