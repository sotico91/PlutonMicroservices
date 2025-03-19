using System.Collections.Generic;
using System.Threading.Tasks;
using MSRecipes.Application.DTOs;

namespace MSRecipes.Application.Interfaces
{
    public interface IRecipeService
    {
        Task<int> CreateRecipeAsync(CreateRecipeDto createRecipeDto);
        Task UpdateRecipeStatusAsync(int id, UpdateRecipeStatusDto updateRecipeStatusDto);
        Task<RecipeDto> GetRecipeByCodeAsync(string code);
        Task<IEnumerable<RecipeDto>> GetRecipesByPatientIdAsync(int patientId);
    }
}
