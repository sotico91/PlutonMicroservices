using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MSPerson.Application.Commands;
using MSPerson.Application.DTOs;
using MSPerson.Application.interfaces;
using MSPerson.Application.Interfaces;
using MSPerson.Domain;
using Xunit;

namespace MSPerson.Tests
{
	public class PersonServiceTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly IPersonService _personService;

        public PersonServiceTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _personService = new PersonService(_personRepositoryMock.Object);
        }

        [Fact]
        public async Task CreatePerson_ShouldAddPerson_WhenValidPersonType()
        {
            var createPersonCommand = new CreatePersonCommand
            {
                Name = "Daniel Cardenas",
                DocumentType = "PS",
                DocumentNumber = "1234588",
                DateOfBirth = new DateTime(1988, 04, 20),
                PhoneNumber = "1234567890",
                Email = "dc@example.com",
                PersonType = PersonType.Patient
            };

            var person = Person.Create(
                createPersonCommand.Name,
                createPersonCommand.DocumentType,
                createPersonCommand.DocumentNumber,
                createPersonCommand.DateOfBirth,
                createPersonCommand.PhoneNumber,
                createPersonCommand.Email,
                createPersonCommand.PersonType
            );

            _personRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Person>()))
                .Returns(Task.CompletedTask)
                .Callback<Person>(p => p.Id = 1);

            var result = await _personService.CreatePerson(createPersonCommand);

            Assert.Equal(1, result);
            _personRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Person>()), Times.Once);
        }

        [Fact]
        public async Task GetAllPersons_ShouldReturnListOfPersons()
        {
            var persons = new List<Person>
            {
                Person.Create("Nestur Alvarez", "PS", "12345678", DateTime.Now, "1234567890", "nesturAlva@example.com", PersonType.Patient),
                Person.Create("Jimena Cortes", "PS", "152545545", DateTime.Now, "587454548", "jimenaCor@example.com", PersonType.Doctor),
            };

            _personRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(persons);

            var result = await _personService.GetAllPersons();

            Assert.Equal(2, result.Count);
            Assert.Equal("Nestur Alvarez", result[0].Name);
            Assert.Equal("Jimena Cortes", result[1].Name);

            _personRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdatePersonAsync_ShouldUpdatePerson_WhenPersonExists()
        {
            var person = Person.Create("Carla Andrade", "PP", "1555585", DateTime.Now, "84111", "carlaAndrade@example.com", PersonType.Patient);

            var updateDto = new UpdatePersonDto
            {
                Name = "Carla Andrade Linares",
                DocumentType = "PS",
                DocumentNumber = "12345458",
                DateOfBirth = new DateTime(1988, 04, 25),
                PhoneNumber = "0987654321",
                Email = "CarlaLinares@example.com",
                PersonType = "Doctor"
            };

            _personRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(person);

            _personRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Person>()))
                .Returns(Task.CompletedTask);

            await _personService.UpdatePersonAsync(1, updateDto);

            _personRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            _personRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Person>()), Times.Once);
        }

        [Fact]
        public async Task DeletePersonAsync_ShouldDeletePerson_WhenPersonExists()
        {
            var personId = 1;
            var existingPerson = Person.Create("John Doe", "ID", "12345678", DateTime.Now, "9876543210", "email@example.com", PersonType.Patient);

            _personRepositoryMock
                .Setup(repo => repo.GetByIdAsync(personId))
                .ReturnsAsync(existingPerson);

            _personRepositoryMock
                .Setup(repo => repo.DeleteAsync(personId))
                .Returns(Task.CompletedTask);

            await _personService.DeletePersonAsync(personId);

            _personRepositoryMock.Verify(repo => repo.GetByIdAsync(personId), Times.Once);
            _personRepositoryMock.Verify(repo => repo.DeleteAsync(personId), Times.Once);
        }

        [Fact]
        public async Task DeletePersonAsync_ShouldThrowKeyNotFoundException_WhenPersonDoesNotExist()
        {
            var personId = 1;

            _personRepositoryMock
                .Setup(repo => repo.GetByIdAsync(personId))
                .ReturnsAsync((Person)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _personService.DeletePersonAsync(personId));

            _personRepositoryMock.Verify(repo => repo.GetByIdAsync(personId), Times.Once);
            _personRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        }   

        [Fact]
        public async Task GetPersonById_ShouldReturnPersonDto()
        {
            var person = Person.Create("Carla Andrade", "PP", "1555585", DateTime.Now, "84111", "carlaAndrade@example.com", PersonType.Patient);
            person.Id = 1;

            _personRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(person);

            var result = await _personService.GetPersonById(1);

            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal(person.Name, result.Name);
            Assert.Equal(person.DocumentType, result.DocumentType);
            Assert.Equal(person.DocumentNumber, result.DocumentNumber);
            Assert.Equal(person.DateOfBirth, result.DateOfBirth);
            Assert.Equal(person.PhoneNumber, result.PhoneNumber);
            Assert.Equal(person.Email, result.Email);
            Assert.Equal(person.PersonType.ToString(), result.PersonType);

            _personRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }
    }
}