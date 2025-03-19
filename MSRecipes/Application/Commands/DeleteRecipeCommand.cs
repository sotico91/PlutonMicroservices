using MediatR;

namespace MSRecipes.Application.Commands
{
    public class DeleteRecipeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}