using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSPerson.Application.DTOs;
using MSPerson.Application.interfaces;
using MSPerson.Application.Interfaces;
using MSPerson.Domain;

namespace MSPerson.Application.Services
{
    public class PersonService : IPersonService
    {

        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public PersonDto ConvertToDto(Person person)
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

        public async Task CreatePerson(CreatePersonDto createPersonDto)
        {
            if (Enum.TryParse(createPersonDto.PersonType, out PersonType personType))
            {
                var person = new Person
                {
                    Name = createPersonDto.Name,
                    DocumentType = createPersonDto.DocumentType,
                    DocumentNumber = createPersonDto.DocumentNumber,
                    DateOfBirth = createPersonDto.DateOfBirth,
                    PhoneNumber = createPersonDto.PhoneNumber,
                    Email = createPersonDto.Email,
                    PersonType = personType
                };

                await _personRepository.AddAsync(person);
            }
            else
            {
                throw new ArgumentException("Invalid PersonType value");
            }
        }

        public async Task<List<PersonDto>> GetAllPersons()
        {
            var persons = await _personRepository.GetAllAsync();
            return persons.Select(ConvertToDto).ToList();
        }

        public async Task UpdatePersonAsync(int Id, UpdatePersonDto updatePersonDto)
        {
            var person = await _personRepository.GetByIdAsync(Id);
            if (person == null)
            {
                throw new ArgumentException("Person not found");
            }

            person.Name = updatePersonDto.Name ?? person.Name;
            person.DocumentType = updatePersonDto.DocumentType ?? person.DocumentType;
            person.DocumentNumber = updatePersonDto.DocumentNumber ?? person.DocumentNumber;
            person.DateOfBirth = updatePersonDto.DateOfBirth != default ? updatePersonDto.DateOfBirth : person.DateOfBirth;
            person.PhoneNumber = updatePersonDto.PhoneNumber ?? person.PhoneNumber;
            person.Email = updatePersonDto.Email ?? person.Email;

            if (!string.IsNullOrEmpty(updatePersonDto.PersonType))
            {
                if (Enum.TryParse<PersonType>(updatePersonDto.PersonType, out PersonType personType) && person.PersonType != personType)
                {
                    person.PersonType = personType;
                }
                else if (!Enum.TryParse<PersonType>(updatePersonDto.PersonType, out _))
                {
                    throw new ArgumentException("Invalid PersonType value");
                }
            }

            await _personRepository.UpdateAsync(person);
        }

        public async Task DeletePersonAsync(int id)
        {
            await _personRepository.DeleteAsync(id);
        }
    }
}