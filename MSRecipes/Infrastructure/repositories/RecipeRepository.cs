using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MSRecipes.Application.Interfaces;
using MSRecipes.Domain;
using MSRecipes.Infrastructure.repositories.Data;

namespace MSRecipes.Infrastructure.repositories
{
	public class RecipeRepository : IRecipeRepository
    {

        private readonly RecipeDbContext _context;

        public RecipeRepository(RecipeDbContext context)
        {
            _context = context;
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }

        public async Task<Recipe> GetByCodeAsync(string code)
        {
            return await _context.Recipes.FirstOrDefaultAsync(r => r.Code == code);
        }

        public async Task<IEnumerable<Recipe>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Recipes.Where(r => r.PatientId == patientId).ToListAsync();
        }

        public async Task AddAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            _context.Entry(recipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}