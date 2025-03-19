using System.Collections.Generic;
using System.Threading.Tasks;
using MSQuotes.Application.DTOs;

namespace MSQuotes.Application.Interfaces
{
    public interface IQuoteService
    {
        Task<int> CreateQuoteAsync(CreateQuoteDto createQuoteDto);
        Task UpdateQuoteStatusAsync(int id, UpdateQuoteStatusDto updateQuoteStatusDto);
        Task<IEnumerable<QuoteDto>> GetAllQuotesAsync();
    }
}
