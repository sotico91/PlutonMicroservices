using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSPerson.Application.interfaces;
using MSPerson.Domain;

namespace MSPerson.Application.Queries
{
    public class GetAllPersonsQuery : IRequest<List<Person>> { }

    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, List<Person>>
    {
        private readonly IPersonRepository _personRepository;

        public GetAllPersonsQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<List<Person>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            return (List<Person>)await _personRepository.GetAllAsync();
        }
    }
}