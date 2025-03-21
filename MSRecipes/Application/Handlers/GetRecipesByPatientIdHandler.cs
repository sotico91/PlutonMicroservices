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
        private readonly IRecipeService _recipeService;

        public GetRecipesByPatientIdHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public async Task<IEnumerable<RecipeDto>> Handle(GetRecipesByPatientIdQuery request, CancellationToken cancellationToken)
        {
            return await _recipeService.GetRecipesByPatientIdAsync(request.PatientId);
        }
    }
}