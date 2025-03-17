using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSRecipes.Application.DTOs;

namespace MSRecipes.Application.Interfaces
{
    public interface IRecipeService
    {
        Task CreateRecipeAsync(CreateRecipeDto createRecipeDto);
        Task UpdateRecipeStatusAsync(int Id, UpdateRecipeStatusDto updateRecipeStatusDto);
        Task<RecipeDto> GetRecipeByCodeAsync(string code);
        Task<List<RecipeDto>> GetRecipesByPatientIdAsync(int patientId);
    }
}
