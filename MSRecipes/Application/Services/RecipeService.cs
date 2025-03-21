using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSRecipes.Application.Commands;
using MSRecipes.Application.DTOs;
using MSRecipes.Application.Interfaces;
using MSRecipes.Domain;

namespace MSRecipes.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;

        public RecipeService(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateRecipeAsync(CreateRecipeCommand createRecipeCommand)
        {
            var recipe = new Recipe
            {
                Code = createRecipeCommand.Code,
                PatientId = createRecipeCommand.PatientId,
                Description = createRecipeCommand.Description,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = createRecipeCommand.ExpiryDate,
                Status = RecipeStatus.Active
            };

            await _repository.AddAsync(recipe);
            return recipe.Id;
        }

        public async Task UpdateRecipeStatusAsync(UpdateRecipeCommand updateRecipeCommand)
        {
            if (!Enum.TryParse(updateRecipeCommand.Status, out RecipeStatus status))
                throw new ArgumentException("Invalid status value");

            var recipe = await _repository.GetByIdAsync(updateRecipeCommand.Id);
            if (recipe == null)
                throw new KeyNotFoundException("Recipe not found");

            recipe.Status = status;
            await _repository.UpdateAsync(recipe);
        }

        public async Task<RecipeDto> GetRecipeByCodeAsync(string code)
        {
            var recipe = await _repository.GetByCodeAsync(code);
            return recipe == null ? null : ConvertToDto(recipe);
        }

        public async Task<IEnumerable<RecipeDto>> GetRecipesByPatientIdAsync(int patientId)
        {
            var recipes = await _repository.GetByPatientIdAsync(patientId);
            return recipes.Select(r => ConvertToDto(r));
        }

    private static RecipeDto ConvertToDto(Recipe recipe)
        {
            return new RecipeDto
            {
                Id = recipe.Id,
                Code = recipe.Code,
                PatientId = recipe.PatientId,
                Description = recipe.Description,
                CreatedDate = recipe.CreatedDate,
                ExpiryDate = recipe.ExpiryDate,
                Status = recipe.Status.ToString()
            };
        }
    }
}