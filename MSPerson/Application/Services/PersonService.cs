using MSPerson.Application.Commands;
using MSPerson.Application.DTOs;
using MSPerson.Application.interfaces;
using MSPerson.Application.Interfaces;
using MSPerson.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
    }

    public async Task<int> CreatePerson(CreatePersonCommand command)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        if (!Enum.TryParse(command.PersonType.ToString(), out PersonType personType))
            throw new ArgumentException("Invalid PersonType value");

        var person = Person.Create(command.Name, command.DocumentType, command.DocumentNumber, command.DateOfBirth,
            command.PhoneNumber, command.Email, personType);

        await _personRepository.AddAsync(person);
        return person.Id;
    }

    public async Task<List<PersonDto>> GetAllPersons()
    {
        var persons = await _personRepository.GetAllAsync();
        return persons.Select(ConvertToDto).ToList();
    }

    public async Task<PersonDto> GetPersonById(int id)
    {
        var person = await _personRepository.GetByIdAsync(id);
        return person != null ? ConvertToDto(person) : null;
    }

    public async Task UpdatePersonAsync(int id, UpdatePersonDto updatePersonDto)
    {
        var person = await _personRepository.GetByIdAsync(id);
        if (person == null) throw new KeyNotFoundException("Person not found");

        PersonType personType = person.PersonType;
        if (!string.IsNullOrWhiteSpace(updatePersonDto.PersonType))
        {
            if (!Enum.TryParse(updatePersonDto.PersonType, out personType))
                throw new ArgumentException("Invalid PersonType value");
        }

        person.Update(updatePersonDto.Name, updatePersonDto.DocumentType, updatePersonDto.DocumentNumber, updatePersonDto.DateOfBirth, updatePersonDto.PhoneNumber, updatePersonDto.Email, personType);

        await _personRepository.UpdateAsync(person);
    }

    public async Task DeletePersonAsync(int id)
    {
       var person = await _personRepository.GetByIdAsync(id);
        if (person == null)
        {
            throw new KeyNotFoundException("Person not found");
        }

        await _personRepository.DeleteAsync(id);
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
