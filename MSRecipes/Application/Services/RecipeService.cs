using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSRecipes.Application.DTOs;
using MSRecipes.Application.Interfaces;
using MSRecipes.Domain;

namespace MSRecipes.Application.Services
{
	public class RecipeService : IRecipeService
	{
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task CreateRecipeAsync(CreateRecipeDto createRecipeDto)
        {
            var recipe = new Recipe
            {
                Code = createRecipeDto.Code,
                PatientId = createRecipeDto.PatientId,
                Description = createRecipeDto.Description,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = createRecipeDto.ExpiryDate,
                Status = RecipeStatus.Active
            };

            await _recipeRepository.AddAsync(recipe);
        }

        public async Task UpdateRecipeStatusAsync(int Id, UpdateRecipeStatusDto updateRecipeStatusDto)
        {
            var recipe = await _recipeRepository.GetByIdAsync(Id);
            if (recipe == null)
            {
                throw new ArgumentException("Recipe not found");
            }

            if (Enum.TryParse(updateRecipeStatusDto.Status, out RecipeStatus status))
            {
                recipe.Status = status;
                await _recipeRepository.UpdateAsync(recipe);
            }
            else
            {
                throw new ArgumentException("Invalid status value");
            }
        }

        public async Task<RecipeDto> GetRecipeByCodeAsync(string code)
        {
            var recipe = await _recipeRepository.GetByCodeAsync(code);
            if (recipe == null)
            {
                throw new ArgumentException("Recipe not found");
            }

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

        public async Task<List<RecipeDto>> GetRecipesByPatientIdAsync(int patientId)
        {
            var recipes = await _recipeRepository.GetByPatientIdAsync(patientId);
            return recipes.Select(r => new RecipeDto
            {
                Id = r.Id,
                Code = r.Code,
                PatientId = r.PatientId,
                Description = r.Description,
                CreatedDate = r.CreatedDate,
                ExpiryDate = r.ExpiryDate,
                Status = r.Status.ToString()
            }).ToList();
        }
    }
}