using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSPerson.Application.Commands;
using MSPerson.Application.interfaces;

namespace MSPerson.Application.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonRepository _personRepository;

        public DeletePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            await _personRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}