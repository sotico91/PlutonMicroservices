using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSPerson.Application.Commands;
using MSPerson.Application.interfaces;
using MSPerson.Domain;

namespace MSPerson.Application.Handlers
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
    {
        private readonly IPersonRepository _personRepository;

        public CreatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = Person.Create(request.Name, request.DocumentType, request.DocumentNumber, request.DateOfBirth,
                                       request.PhoneNumber, request.Email, request.PersonType);

            await _personRepository.AddAsync(person);
            return person.Id;
        }
    }
}