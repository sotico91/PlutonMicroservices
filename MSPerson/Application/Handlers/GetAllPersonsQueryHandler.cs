using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSPerson.Application.DTOs;
using MSPerson.Application.Interfaces;
using MSPerson.Application.Queries;

namespace MSPerson.Application.Handlers
{
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, List<PersonDto>>
    {
        private readonly IPersonService _personService;

        public GetAllPersonsQueryHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<List<PersonDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            return await _personService.GetAllPersons();
        }
    }
}