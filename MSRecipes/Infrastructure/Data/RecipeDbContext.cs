using System.Data.Entity;
using MSRecipes.Domain;

namespace MSRecipes.Infrastructure.repositories.Data
{
	public class RecipeDbContext : DbContext
    {
        public RecipeDbContext()
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Status)
                .IsRequired();
        }
    }
}