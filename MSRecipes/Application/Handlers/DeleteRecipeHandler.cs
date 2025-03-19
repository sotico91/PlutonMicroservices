using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSRecipes.Application.Commands;
using MSRecipes.Application.Interfaces;

namespace MSRecipes.Application.Handlers
{
    public class DeleteRecipeHandler : IRequestHandler<DeleteRecipeCommand, bool>
    {
        private readonly IRecipeRepository _repository;

        public DeleteRecipeHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _repository.GetByIdAsync(request.Id);
            if (recipe == null) return false;

            await _repository.DeleteAsync(recipe.Id);
            return true;
        }
    }
}