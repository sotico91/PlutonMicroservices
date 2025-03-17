using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using MSQuotes.Application.Interfaces;
using MSQuotes.Domain;
using MSQuotes.Infrastructure.Data;

namespace MSQuotes.Infrastructure.Repositories
{
	public class QuoteRepository : IQuoteRepository
    {
        private readonly QuoteDbContext _context;

        public QuoteRepository(QuoteDbContext context)
        {
            _context = context;
        }

        public async Task<Quote> GetByIdAsync(int id)
        {
            return await _context.Quotes.FindAsync(id);
        }

        public async Task<IEnumerable<Quote>> GetAllAsync()
        {
            return await _context.Quotes.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(Quote quote)
        {
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Quote quote)
        {
            _context.Entry(quote).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote != null)
            {
                _context.Quotes.Remove(quote);
                await _context.SaveChangesAsync();
            }
        }
    }

}