using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSRecipes.Application.Commands;
using MSRecipes.Application.Interfaces;

namespace MSRecipes.Application.Handlers
{
    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, int>
    {
        private readonly IRecipeService _recipeService;

        public CreateRecipeHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public async Task<int> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            return await _recipeService.CreateRecipeAsync(request);
        }
    }
}