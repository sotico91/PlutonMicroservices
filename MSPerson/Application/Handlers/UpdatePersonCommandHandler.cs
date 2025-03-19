using System;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSPerson.Application.interfaces;
using System.Collections.Generic;

namespace MSPerson.Application.Commands
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
    {
        private readonly IPersonRepository _personRepository;

        public UpdatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.Id);
            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {request.Id} not found.");
            }

            person.Name = request.Name ?? person.Name;
            person.DocumentType = request.DocumentType ?? person.DocumentType;
            person.DocumentNumber = request.DocumentNumber ?? person.DocumentNumber;
            person.DateOfBirth = (DateTime)(request.DateOfBirth != default ? request.DateOfBirth : person.DateOfBirth);
            person.PhoneNumber = request.PhoneNumber ?? person.PhoneNumber;
            person.Email = request.Email ?? person.Email;

            var personType = request.PersonType != null && request.PersonType != person.PersonType ? request.PersonType : person.PersonType;
            person.Update(person.Name, person.DocumentType, person.DocumentNumber, person.DateOfBirth, person.PhoneNumber, person.Email, (Domain.PersonType)personType);

            await _personRepository.UpdateAsync(person);
            return true;
        }
    }
}