using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using MSQuotes.Application.Commands;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Queries;
using MSQuotes.Domain;

namespace MSQuotes.API.Controllers
{
    [RoutePrefix("api/quotes")]
    public class QuotesController : ApiController
    {
        private readonly IMediator _mediator;

        public QuotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateQuoteDto createQuoteDto)
        {
            try
            {
                var quoteId = await _mediator.Send(new CreateQuoteCommand
                {
                    Date = createQuoteDto.Date,
                    Location = createQuoteDto.Location,
                    PatientId = createQuoteDto.PatientId,
                    DoctorId = createQuoteDto.DoctorId
                });

                var createdQuote = new
                {
                    Id = quoteId,
                    createQuoteDto.Date,
                    createQuoteDto.Location,
                    createQuoteDto.PatientId,
                    createQuoteDto.DoctorId
                };

                return Content(HttpStatusCode.Created, createdQuote);
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

                await _mediator.Send(new UpdateQuoteStatusCommand
                {
                    Id = id,
                    Code = updateQuoteStatusDto.Code,
                    Description = updateQuoteStatusDto.Description,
                    ExpiryDate = updateQuoteStatusDto.ExpiryDate,
                    Status = updateQuoteStatusDto.Status
                });
                return StatusCode(System.Net.HttpStatusCode.OK);
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
                var quotes = await _mediator.Send(new GetAllQuotesQuery());

                var quoteDtos = quotes.Select(q => new QuoteDto
                {
                    Id = q.Id,
                    Date = q.Date,
                    Location = q.Location,
                    PatientId = q.PatientId,
                    DoctorId = q.DoctorId,
                    Status = Enum.GetName(typeof(QuoteStatus), q.Status)
                }).ToList();

                return Ok(quoteDtos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}