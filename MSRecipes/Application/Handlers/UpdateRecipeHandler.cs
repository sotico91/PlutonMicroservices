using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSRecipes.Application.Commands;
using MSRecipes.Application.Interfaces;

namespace MSRecipes.Application.Handlers
{
    public class UpdateRecipeHandler : IRequestHandler<UpdateRecipeCommand, bool>
    {
        private readonly IRecipeService _recipeService;

        public UpdateRecipeHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public async Task<bool> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            await _recipeService.UpdateRecipeStatusAsync(request);
            return true;
        }
    }
}