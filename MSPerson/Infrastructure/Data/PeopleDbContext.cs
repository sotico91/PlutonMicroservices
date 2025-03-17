using System.Data.Entity;
using MSPerson.Domain;

namespace MSPerson.Infrastructure.Data
{
	public class PeopleDbContext : DbContext
	{

        public PeopleDbContext()
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Person> People { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .Property(p => p.PersonType)
                .IsRequired();
        }
    }

}