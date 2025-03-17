
using System.Data.Entity;
using MSQuotes.Domain;

namespace MSQuotes.Infrastructure.Data
{
	public class QuoteDbContext : DbContext
    {

        public QuoteDbContext()
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quote>()
                .Property(q => q.Status)
                .IsRequired();
        }
    }
}