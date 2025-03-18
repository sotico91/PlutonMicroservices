using System.Collections.Generic;
using System.Threading.Tasks;
using MSRecipes.Domain;

namespace MSRecipes.Application.Interfaces
{
	public interface IRecipeRepository
	{
        Task<Recipe> GetByIdAsync(int id);
        Task<Recipe> GetByCodeAsync(string code);
        Task<IEnumerable<Recipe>> GetByPatientIdAsync(int patientId);
        Task AddAsync(Recipe recipe);
        Task UpdateAsync(Recipe recipe);
        Task DeleteAsync(int id);

    }
}