using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSRecipes.Application.Commands;
using MSRecipes.Application.Interfaces;
using MSRecipes.Domain;
using System;

namespace MSRecipes.Application.Handlers
{
    public class UpdateRecipeHandler : IRequestHandler<UpdateRecipeCommand, bool>
    {
        private readonly IRecipeRepository _repository;

        public UpdateRecipeHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _repository.GetByIdAsync(request.Id);
            if (recipe == null) return false;

            if (!Enum.TryParse(request.Status, out RecipeStatus status))
            {
                return false;
            }

            recipe.Update(recipe.Id, recipe.Code, recipe.PatientId, recipe.Description, recipe.CreatedDate, recipe.ExpiryDate, status);
            await _repository.UpdateAsync(recipe);
            return true;
        }
    }
}