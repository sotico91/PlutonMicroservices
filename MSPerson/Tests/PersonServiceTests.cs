using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Moq;
using MSPerson.Application.Commands;
using MSPerson.Application.DTOs;
using MSPerson.Application.Interfaces;
using MSPerson.Application.Queries;
using MSPerson.Application.Services;
using MSPerson.Domain;
using Xunit;

namespace MSPerson.Tests
{
	public class PersonServiceTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IPersonService> _personServiceMock;
        private readonly IPersonService _personService;

        public PersonServiceTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _personServiceMock = new Mock<IPersonService>();
            _personService = new PersonService(_mediatorMock.Object);
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


            _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePersonCommand>(), default))
                  .ReturnsAsync(1);

            var result = await _personService.CreatePerson(createPersonDto);

            Assert.Equal(1, result);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreatePersonCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task GetAllPersons_ShouldReturnListOfPersons()
        {
            var persons = new List<Person>
            {
                Person.Create("Nestur Alvarez", "PS", "12345678", DateTime.Now, "1234567890", "nesturAlva@example.com", PersonType.Patient),
                Person.Create("Jimena Cortes", "PS", "152545545", DateTime.Now, "587454548", "jimenaCor@example.com", PersonType.Doctor),
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllPersonsQuery>(), default)).ReturnsAsync(persons);

            var result = await _personService.GetAllPersons();

            Assert.Equal(2, result.Count);
            Assert.Equal("Nestur Alvarez", result[0].Name);
            Assert.Equal("Jimena Cortes", result[1].Name);

            _mediatorMock.Verify(m => m.Send(It.IsAny<GetAllPersonsQuery>(), default), Times.Once);
        }

        [Fact]
        public async Task UpdatePersonAsync_ShouldUpdatePerson_WhenPersonExists()
        {
            var person = Person.Create("Carla Andrade", "PP", "1555585", DateTime.Now, "84111", "carlaAndrade@example.com", PersonType.Patient);

            var updateDto = new UpdatePersonDto
            {
                Name = "Carla Andrade linares",
                DocumentType = "PS",
                DocumentNumber = "12345458",
                DateOfBirth = new DateTime(1988, 04, 25),
                PhoneNumber = "0987654321",
                Email = "CarlaLinares@example.com",
                PersonType = "Doctor"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePersonCommand>(), default)).ReturnsAsync(true);

            await _personService.UpdatePersonAsync(1, updateDto);

            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdatePersonCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task DeletePersonAsync_ShouldDeletePerson()
        {
            var personId = 1;

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeletePersonCommand>(), default))
             .ReturnsAsync(true);

            await _personService.DeletePersonAsync(personId);

            _mediatorMock.Verify(m => m.Send(It.IsAny<DeletePersonCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task ConvertToDto_ShouldReturnPersonDtoAsync()
        {
            var person = Person.Create("Carla Andrade", "PP", "1555585", DateTime.Now, "84111", "carlaAndrade@example.com", PersonType.Patient);
            
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPersonByIdQuery>(), default)).ReturnsAsync(person);

            var result = await _personService.GetPersonById(person.Id);
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal(person.Name, result.Name);
            Assert.Equal(person.DocumentType, result.DocumentType);
            Assert.Equal(person.DocumentNumber, result.DocumentNumber);
            Assert.Equal(person.DateOfBirth, result.DateOfBirth);
            Assert.Equal(person.PhoneNumber, result.PhoneNumber);
            Assert.Equal(person.Email, result.Email);
            Assert.Equal(person.PersonType.ToString(), result.PersonType);

            _mediatorMock.Verify(m => m.Send(It.IsAny<GetPersonByIdQuery>(), default), Times.Once);
        }
    }
}