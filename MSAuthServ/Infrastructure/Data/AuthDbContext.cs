
using System.Data.Entity;
using MSAuthServ.Domain;

namespace MSAuthServ.Infrastructure.Data
{
    public class AuthDbContext : DbContext
    {

        public AuthDbContext() : base("AuthDbContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<User> Users { get; set; }
    }
}