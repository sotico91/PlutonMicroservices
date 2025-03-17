
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSPerson.Application.interfaces;
using MSPerson.Application.Queries;
using MSPerson.Domain;

namespace MSPerson.Application.Handlers
{
	public class GetPersonByIdHandler: IRequestHandler<GetPersonByIdQuery, Person>
	{

        private readonly IPersonRepository _repository;
        public GetPersonByIdHandler(IPersonRepository repository)
        {
            _repository = repository;
        }
        public async Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}