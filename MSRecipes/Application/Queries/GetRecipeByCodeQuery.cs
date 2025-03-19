using MediatR;
using MSRecipes.Application.DTOs;

namespace MSRecipes.Application.Queries
{
    public class GetRecipeByCodeQuery : IRequest<RecipeDto>
    {
        public string Code { get; }

    public GetRecipeByCodeQuery(string code)
    {
        Code = code;
    }
}
}