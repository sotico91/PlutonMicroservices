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
        private readonly IRecipeService _recipeService;

        public GetRecipeByCodeHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public async Task<RecipeDto> Handle(GetRecipeByCodeQuery request, CancellationToken cancellationToken)
        {
            var recipeDto = await _recipeService.GetRecipeByCodeAsync(request.Code);
            if (recipeDto == null)
            {
                throw new ArgumentException("Recipe not found");
            }

            return recipeDto;
        }
    }
}