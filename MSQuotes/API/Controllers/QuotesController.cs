using System;
using System.Threading.Tasks;
using System.Web.Http;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Interfaces;

namespace MSQuotes.API.Controllers
{
    [RoutePrefix("api/quotes")]
    public class QuotesController : ApiController
    {
        private readonly IQuoteService _quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateQuoteDto createQuoteDto)
        {
            try
            {
                await _quoteService.CreateQuoteAsync(createQuoteDto);
                return Created("", createQuoteDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("{id}/status")]
        public async Task<IHttpActionResult> UpdateStatus(int id, [FromBody] UpdateQuoteStatusDto updateQuoteStatusDto)
        {
            try
            {
                await _quoteService.UpdateQuoteStatusAsync(id, updateQuoteStatusDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var quotes = await _quoteService.GetAllQuotesAsync();
                return Ok(quotes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}