using System;
using MediatR;

namespace MSRecipes.Application.Commands
{
    public class CreateRecipeCommand : IRequest<int>
    {
        public int PatientId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}