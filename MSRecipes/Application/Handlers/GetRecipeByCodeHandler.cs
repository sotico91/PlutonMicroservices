using System;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSRecipes.Application.DTOs;
using MSRecipes.Application.Interfaces;
using MSRecipes.Application.Queries;

namespace MSRecipes.Application.Handlers
{
    public class GetRecipeByCodeHandler : IRequestHandler<GetRecipeByCodeQuery, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetRecipeByCodeHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<RecipeDto> Handle(GetRecipeByCodeQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.GetByCodeAsync(request.Code);
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
    }
}