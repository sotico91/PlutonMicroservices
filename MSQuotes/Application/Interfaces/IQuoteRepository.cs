using System.Collections.Generic;
using System.Threading.Tasks;
using MSQuotes.Domain;

namespace MSQuotes.Application.Interfaces
{
	public interface IQuoteRepository
	{
        Task<Quote> GetByIdAsync(int id);
        Task<IEnumerable<Quote>> GetAllAsync();
        Task AddAsync(Quote quote);
        Task UpdateAsync(Quote quote);
        Task DeleteAsync(int id);
    }
}