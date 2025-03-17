using System;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using MSPerson.Application.DTOs;
using MSPerson.Application.Interfaces;
using MSPerson.Application.Queries;

namespace MSPerson.API
{
    [RoutePrefix("api/people")]
    
    public class PeopleController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IPersonService _personService;
        public PeopleController(IMediator mediator, IPersonService personService)
        {
            _mediator = mediator;
            _personService = personService;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var token = Request.Headers.Authorization?.Parameter;
            Console.WriteLine("Token recibido: " + token);

            var person = await _mediator.Send(new GetPersonByIdQuery(id));
            if (person == null) return NotFound();
            var personDto = _personService.ConvertToDto(person);
            return Ok(person);
        }

        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {

            var token = Request.Headers.Authorization?.Parameter;
            Console.WriteLine("Token recibido: " + token);

            try
            {
                var persons = await _personService.GetAllPersons();
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreatePersonDto createPersonDto)
        {
            try
            {
                await _personService.CreatePerson(createPersonDto);
                return Created("", createPersonDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int Id, [FromBody] UpdatePersonDto updatePersonDto)
        {
            try
            {
                await _personService.UpdatePersonAsync(Id, updatePersonDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                await _personService.DeletePersonAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
