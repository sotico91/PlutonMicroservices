using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MSPerson.Application.Commands;
using MSPerson.Application.DTOs;
using MSPerson.Application.Interfaces;
using MSPerson.Application.Queries;
using MSPerson.Domain;
using MSPerson.Domain.Factories;

namespace MSPerson.Application.Services
{
    public class PersonService : IPersonService
    {

        private readonly IMediator _mediator;

        public PersonService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<int> CreatePerson(CreatePersonDto createPersonDto)
        {

            if (createPersonDto == null)
                throw new ArgumentNullException(nameof(createPersonDto));

            if (!Enum.TryParse(createPersonDto.PersonType, out PersonType personType))
                throw new ArgumentException("Invalid PersonType value");

            var command = new CreatePersonCommand
            {
                Name = createPersonDto.Name,
                DocumentType = createPersonDto.DocumentType,
                DocumentNumber = createPersonDto.DocumentNumber,
                DateOfBirth = createPersonDto.DateOfBirth,
                PhoneNumber = createPersonDto.PhoneNumber,
                Email = createPersonDto.Email,
                PersonType = personType
            };

            return await _mediator.Send(command);
        }

        public async Task<List<PersonDto>> GetAllPersons()
        {
            var persons = await _mediator.Send(new GetAllPersonsQuery());
            return persons.Select(ConvertToDto).ToList();
        }

        public async Task<PersonDto> GetPersonById(int id)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(id));
            return person != null ? ConvertToDto(person) : null;
        }

        public async Task UpdatePersonAsync(int id, UpdatePersonDto updatePersonDto)
        {
            if (!Enum.TryParse(updatePersonDto.PersonType, out PersonType personType))
                throw new ArgumentException("Invalid PersonType value");

            var command = new UpdatePersonCommand
            {
                Id = id,
                Name = updatePersonDto.Name,
                DocumentType = updatePersonDto.DocumentType,
                DocumentNumber = updatePersonDto.DocumentNumber,
                DateOfBirth = updatePersonDto.DateOfBirth,
                PhoneNumber = updatePersonDto.PhoneNumber,
                Email = updatePersonDto.Email,
                PersonType = personType
            };

            await _mediator.Send(command);
        }

        public async Task DeletePersonAsync(int id)
        {
            await _mediator.Send(new DeletePersonCommand(id));
        }

        private static PersonDto ConvertToDto(Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                DocumentType = person.DocumentType,
                DocumentNumber = person.DocumentNumber,
                DateOfBirth = person.DateOfBirth,
                PhoneNumber = person.PhoneNumber,
                Email = person.Email,
                PersonType = person.PersonType.ToString()
            };
        }
    }
}