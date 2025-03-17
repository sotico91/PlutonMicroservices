using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MSPerson.Application.DTOs;
using MSPerson.Application.interfaces;
using MSPerson.Application.Services;
using MSPerson.Domain;
using Xunit;

namespace MSPerson.Tests
{
	public class PersonServiceTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly PersonService _personService;

        public PersonServiceTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _personService = new PersonService(_personRepositoryMock.Object);
        }

        [Fact]
        public async Task CreatePerson_ShouldAddPerson_WhenValidPersonType()
        {
            var createPersonDto = new CreatePersonDto
            {
                Name = "Daniel Cardenas",
                DocumentType = "PS",
                DocumentNumber = "1234588",
                DateOfBirth = new DateTime(1988, 04, 20),
                PhoneNumber = "1234567890",
                Email = "dc@example.com",
                PersonType = "Patient"
            };

            await _personService.CreatePerson(createPersonDto);

            _personRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Person>()), Times.Once);
        }

        [Fact]
        public async Task GetAllPersons_ShouldReturnListOfPersons()
        {
            var persons = new List<Person>
            {
                new Person { Id = 1, Name = "Daniel Cardenas", PersonType = PersonType.Doctor },
                new Person { Id = 2, Name = "Jhon Torres", PersonType = PersonType.Patient }
            };

            _personRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(persons);

            var result = await _personService.GetAllPersons();

            Assert.Equal(2, result.Count);
            Assert.Equal("Daniel Cardenas", result[0].Name);
            Assert.Equal("Jhon Torres", result[1].Name);
        }

        [Fact]
        public async Task UpdatePersonAsync_ShouldUpdatePerson_WhenPersonExists()
        {
            var person = new Person { Id = 1, Name = "Carla Andrade", PersonType = PersonType.Doctor };

            var updateDto = new UpdatePersonDto
            {
                Name = "Carla Andrade linares",
                DocumentType = "ps",
                DocumentNumber = "12345458",
                DateOfBirth = new DateTime(1988, 04, 25),
                PhoneNumber = "0987654321",
                Email = "CarlaLinares@example.com",
                PersonType = "Patient"
            };

            _personRepositoryMock.Setup(repo => repo.GetByIdAsync(person.Id)).ReturnsAsync(person);

            await _personService.UpdatePersonAsync(person.Id, updateDto);

            _personRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Person>()), Times.Once);
        }

        [Fact]
        public async Task DeletePersonAsync_ShouldDeletePerson()
        {
            var personId = 1;

            await _personService.DeletePersonAsync(personId);

            _personRepositoryMock.Verify(repo => repo.DeleteAsync(personId), Times.Once);
        }

        [Fact]
        public void ConvertToDto_ShouldReturnPersonDto()
        {
            var person = new Person
            {
                Id = 1,
                Name = "Patricia Garcia",
                DocumentType = "PS",
                DocumentNumber = "123454589",
                DateOfBirth = new DateTime(1985, 04, 20),
                PhoneNumber = "123456714",
                Email = "patico1988@example.com",
                PersonType = PersonType.Patient
            };

            var result = _personService.ConvertToDto(person);

            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal(person.Name, result.Name);
        }
    }
}