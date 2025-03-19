using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSPerson.Application.interfaces;
using MSPerson.Application.Queries;
using MSPerson.Domain;

namespace MSPerson.Application.Handlers
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Person>
    {
        private readonly IPersonRepository _personRepository;

        public GetPersonByIdQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            return await _personRepository.GetByIdAsync(request.Id);
        }
    }
}