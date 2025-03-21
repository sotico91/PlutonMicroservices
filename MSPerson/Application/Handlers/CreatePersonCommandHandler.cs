using MediatR;
using MSPerson.Application.Commands;
using MSPerson.Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
{
    private readonly IPersonService _personService;

    public CreatePersonCommandHandler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        return await _personService.CreatePerson(request);
    }
}
