using System.Collections.Generic;
using MediatR;
using MSRecipes.Application.DTOs;

namespace MSRecipes.Application.Queries
{
    public class GetRecipesByPatientIdQuery : IRequest<IEnumerable<RecipeDto>>
    {
        public int PatientId { get; set; }

        public GetRecipesByPatientIdQuery(int patientId)
        {
            PatientId = patientId;
        }
    }
}