using System;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSPerson.Application.DTOs;
using MSPerson.Application.Interfaces;

namespace MSPerson.Application.Commands
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
    {
        private readonly IPersonService _personService;

        public UpdatePersonCommandHandler(IPersonService personService)
        {
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));
        }

        public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var updatePersonDto = new UpdatePersonDto
            {
                Name = request.Name,
                DocumentType = request.DocumentType,
                DocumentNumber = request.DocumentNumber,
                DateOfBirth = (DateTime)request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                PersonType = request.PersonType.ToString()
            };

            await _personService.UpdatePersonAsync(request.Id, updatePersonDto);
            return true;
        }
    }
}