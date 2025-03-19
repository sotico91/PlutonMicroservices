using MediatR;

namespace MSRecipes.Application.Commands
{
    public class UpdateRecipeCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}