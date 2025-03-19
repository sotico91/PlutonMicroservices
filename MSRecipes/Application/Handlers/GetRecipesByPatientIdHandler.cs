using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSRecipes.Application.DTOs;
using MSRecipes.Application.Interfaces;
using MSRecipes.Application.Queries;

namespace MSRecipes.Application.Handlers
{
    public class GetRecipesByPatientIdHandler : IRequestHandler<GetRecipesByPatientIdQuery, IEnumerable<RecipeDto>>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetRecipesByPatientIdHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<RecipeDto>> Handle(GetRecipesByPatientIdQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _recipeRepository.GetByPatientIdAsync(request.PatientId);

            return recipes.Select(recipe => new RecipeDto
            {
                Id = recipe.Id,
                Code = recipe.Code,
                PatientId = recipe.PatientId,
                Description = recipe.Description,
                CreatedDate = recipe.CreatedDate,
                ExpiryDate = recipe.ExpiryDate,
                Status = recipe.Status.ToString()
            }).ToList();
        }
    }
}