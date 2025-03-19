using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MSRecipes.Application.Commands;
using MSRecipes.Application.DTOs;
using MSRecipes.Application.Interfaces;
using MSRecipes.Application.Queries;
using MSRecipes.Domain;

namespace MSRecipes.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMediator _mediator;

        public RecipeService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> CreateRecipeAsync(CreateRecipeDto createRecipeDto)
        {
            var command = new CreateRecipeCommand
            {
                Code = createRecipeDto.Code,
                PatientId = createRecipeDto.PatientId,
                Description = createRecipeDto.Description,
                ExpiryDate = createRecipeDto.ExpiryDate
            };

            return await _mediator.Send(command);
        }

        public async Task UpdateRecipeStatusAsync(int id, UpdateRecipeStatusDto updateRecipeStatusDto)
        {
            if (!Enum.TryParse(updateRecipeStatusDto.Status, out RecipeStatus status))
                throw new ArgumentException("Invalid status value");

            var command = new UpdateRecipeCommand
            {
                Id = id,
                Status = status.ToString()
            };

            await _mediator.Send(command);
        }

        public async Task<RecipeDto> GetRecipeByCodeAsync(string code)
        {
            return await _mediator.Send(new GetRecipeByCodeQuery(code));
        }

        public async Task<IEnumerable<RecipeDto>> GetRecipesByPatientIdAsync(int patientId)
        {
            return await _mediator.Send(new GetRecipesByPatientIdQuery(patientId));
        }
    }
}