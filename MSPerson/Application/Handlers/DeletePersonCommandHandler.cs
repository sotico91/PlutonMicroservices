using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSPerson.Application.Commands;
using MSPerson.Application.Interfaces;

namespace MSPerson.Application.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonService _personService;

        public DeletePersonCommandHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            await _personService.DeletePersonAsync(request.Id);
            return true;
        }
    }
}