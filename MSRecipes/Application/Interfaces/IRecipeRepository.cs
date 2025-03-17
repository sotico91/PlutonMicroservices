using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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