using System;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using MSRecipes.Application.Commands;
using MSRecipes.Application.DTOs;
using MSRecipes.Application.Queries;

namespace MSRecipes.API.Controllers
{
    [RoutePrefix("api/recipes")]
    public class RecipesController : ApiController
    {
        private readonly IMediator _mediator;

        public RecipesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateRecipeDto createRecipeDto)
        {
            try
            {
                var command = new CreateRecipeCommand
                {
                    Code = createRecipeDto.Code,
                    PatientId = createRecipeDto.PatientId,
                    Description = createRecipeDto.Description,
                    ExpiryDate = createRecipeDto.ExpiryDate
                };

                var result = await _mediator.Send(command);
                return Created("", result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("{id}/status")]
        public async Task<IHttpActionResult> UpdateStatus(int id, [FromBody] UpdateRecipeStatusDto updateRecipeStatusDto)
        {
            try
            {
                var command = new UpdateRecipeCommand
                {
                    Id = id,
                    Status = updateRecipeStatusDto.Status
                };

                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{code}")]
        public async Task<IHttpActionResult> GetByCode(string code)
        {
            try
            {
                var query = new GetRecipeByCodeQuery(code);
                var recipe = await _mediator.Send(query);
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("patient/{patientId}")]
        public async Task<IHttpActionResult> GetByPatientId(int patientId)
        {
            try
            {
                var query = new GetRecipesByPatientIdQuery(patientId);
                var recipes = await _mediator.Send(query);
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}