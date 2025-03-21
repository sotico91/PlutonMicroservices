using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSPerson.Application.Interfaces;
using MSPerson.Application.Queries;
using MSPerson.Application.DTOs;

namespace MSPerson.Application.Handlers
{
    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonDto>
    {
        private readonly IPersonService _personService;

        public GetPersonByIdHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            return await _personService.GetPersonById(request.Id);
        }
    }
}
