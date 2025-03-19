using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSRecipes.Application.Commands;
using MSRecipes.Application.Interfaces;
using MSRecipes.Domain;
using System;

namespace MSRecipes.Application.Handlers
{
    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, int>
    {
        private readonly IRecipeRepository _repository;

        public CreateRecipeHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = new Recipe
            {
                Code = request.Code,
                PatientId = request.PatientId,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = request.ExpiryDate,
                Status = RecipeStatus.Active
            };

            await _repository.AddAsync(recipe);
            return recipe.Id;
        }
    }
}