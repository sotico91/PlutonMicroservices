using System.Collections.Generic;
using System.Threading.Tasks;
using MSRecipes.Application.Commands;
using MSRecipes.Application.DTOs;

namespace MSRecipes.Application.Interfaces
{
    public interface IRecipeService
    {
        Task<int> CreateRecipeAsync(CreateRecipeCommand createRecipeCommand);
        Task UpdateRecipeStatusAsync(UpdateRecipeCommand updateRecipeCommand);
        Task<RecipeDto> GetRecipeByCodeAsync(string code);
        Task<IEnumerable<RecipeDto>> GetRecipesByPatientIdAsync(int patientId);
    }
}
