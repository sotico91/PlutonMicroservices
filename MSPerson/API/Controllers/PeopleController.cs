using System;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using MSPerson.Application.Commands;
using MSPerson.Application.DTOs;
using MSPerson.Application.Queries;
using MSPerson.Domain;

namespace MSPerson.API
{
    [RoutePrefix("api/people")]
    
    public class PeopleController : ApiController
    {
        private readonly IMediator _mediator;

        public PeopleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}", Name = "GetPersonById")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var person = await _mediator.Send(new GetPersonByIdQuery(id));
                if (person == null) return NotFound();

                return Ok(person);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {   
            try
            {
                var persons = await _mediator.Send(new GetAllPersonsQuery());
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
                if (!Enum.TryParse(createPersonDto.PersonType, out PersonType personType))
                {
                    return BadRequest("Invalid PersonType value.");
                }

                var personId = await _mediator.Send(new CreatePersonCommand
                {
                    Name = createPersonDto.Name,
                    DocumentType = createPersonDto.DocumentType,
                    DocumentNumber = createPersonDto.DocumentNumber,
                    DateOfBirth = createPersonDto.DateOfBirth,
                    PhoneNumber = createPersonDto.PhoneNumber,
                    Email = createPersonDto.Email,
                    PersonType = personType
                });

                return CreatedAtRoute("GetPersonById", new { id = personId }, createPersonDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdatePersonDto updatePersonDto)
        {
            try
            {

                PersonType? personType = null;
                if (!string.IsNullOrWhiteSpace(updatePersonDto.PersonType))
                {
                    if (Enum.TryParse(updatePersonDto.PersonType, out PersonType parsedPersonType))
                    {
                        personType = parsedPersonType;
                    }
                }

                await _mediator.Send(new UpdatePersonCommand
                {
                    Id = id,
                    Name = updatePersonDto.Name,
                    DocumentType = updatePersonDto.DocumentType,
                    DocumentNumber = updatePersonDto.DocumentNumber,
                    DateOfBirth = updatePersonDto.DateOfBirth,
                    PhoneNumber = updatePersonDto.PhoneNumber,
                    Email = updatePersonDto.Email,
                    PersonType = personType
                });

                return StatusCode(System.Net.HttpStatusCode.OK);
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
                await _mediator.Send(new DeletePersonCommand(id));
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
