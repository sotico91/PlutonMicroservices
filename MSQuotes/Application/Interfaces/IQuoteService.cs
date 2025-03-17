using System.Collections.Generic;
using System.Threading.Tasks;
using MSQuotes.Application.DTOs;

namespace MSQuotes.Application.Interfaces
{
    public interface IQuoteService
    {
        Task CreateQuoteAsync(CreateQuoteDto createQuoteDto);
        Task UpdateQuoteStatusAsync(int Id, UpdateQuoteStatusDto updateQuoteStatusDto);
        Task<List<QuoteDto>> GetAllQuotesAsync();
    }
}
