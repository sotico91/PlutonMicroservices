using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MSQuotes.Application.Interfaces;
using MSQuotes.Domain;
using Xunit;

namespace MSQuotes.Tests
{
    public class IQuoteRepositoryTests
    {
        private readonly Mock<IQuoteRepository> _mockRepository;

        public IQuoteRepositoryTests()
        {
            _mockRepository = new Mock<IQuoteRepository>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnQuote()
        {

            var quote = new Quote { Id = 1, Status = QuoteStatus.Pending };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(quote);

            var result = await _mockRepository.Object.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Pending", result.Status.ToString());
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnQuotes()
        {
   
            var quotes = new List<Quote>
        {
            new Quote { Id = 1, Status = QuoteStatus.InProgress },
            new Quote { Id = 2,Status = QuoteStatus.Completed }
        };
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(quotes);

   
            var result = await _mockRepository.Object.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddAsync_ShouldCallAddOnce()
        {

            var quote = new Quote { Id = 3, Status = QuoteStatus.InProgress };

            await _mockRepository.Object.AddAsync(quote);

      
            _mockRepository.Verify(repo => repo.AddAsync(quote), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallUpdateOnce()
        {
      
            var quote = new Quote { Id = 1, Status = QuoteStatus.Pending };

            await _mockRepository.Object.UpdateAsync(quote);

            _mockRepository.Verify(repo => repo.UpdateAsync(quote), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallDeleteOnce()
        {
            await _mockRepository.Object.DeleteAsync(1);

            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}