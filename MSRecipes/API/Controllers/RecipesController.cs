using System;
using System.Threading.Tasks;
using System.Web.Http;
using MSRecipes.Application.DTOs;
using MSRecipes.Application.Interfaces;

namespace MSRecipes.API.Controllers
{
    [RoutePrefix("api/recipes")]
    public class RecipesController : ApiController
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateRecipeDto createRecipeDto)
        {
            try
            {
                await _recipeService.CreateRecipeAsync(createRecipeDto);
                return Created("", createRecipeDto);
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
                await _recipeService.UpdateRecipeStatusAsync(id, updateRecipeStatusDto);
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
                var recipe = await _recipeService.GetRecipeByCodeAsync(code);
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
                var recipes = await _recipeService.GetRecipesByPatientIdAsync(patientId);
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}