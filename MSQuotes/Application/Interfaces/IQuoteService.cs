using System.Collections.Generic;
using System.Threading.Tasks;
using MSQuotes.Application.Commands;
using MSQuotes.Application.DTOs;

namespace MSQuotes.Application.Interfaces
{
    public interface IQuoteService
    {
        Task<int> CreateQuoteAsync(CreateQuoteCommand createCommand);
        Task UpdateQuoteStatusAsync(UpdateQuoteStatusCommand updateQuoteStatusDto);
        Task<IEnumerable<QuoteDto>> GetAllQuotesAsync();
    }
}
