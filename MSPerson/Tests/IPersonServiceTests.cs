using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MSPerson.Application.DTOs;
using MSPerson.Application.Interfaces;
using MSPerson.Domain;
using Xunit;

namespace MSPerson.Tests
{
	public class IPersonServiceTests
	{
        private readonly Mock<IPersonService> _personServiceMock;

        public IPersonServiceTests()
        {
            _personServiceMock = new Mock<IPersonService>();
        }

        [Fact]
        public async Task GetAllPersons_ShouldReturnListOfPersonDto()
        {
            var expectedPersons = new List<PersonDto>
            {
                new PersonDto { Id = 1, Name = "Miguel" },
                new PersonDto { Id = 2, Name = "Lina" }
            };

            _personServiceMock.Setup(service => service.GetAllPersons()).ReturnsAsync(expectedPersons);

            var result = await _personServiceMock.Object.GetAllPersons();

            Assert.NotNull(result);
            Assert.Equal(expectedPersons.Count, result.Count);
        }

        [Fact]
        public async Task CreatePerson_ShouldCallServiceOnce()
        {
            var createPersonDto = new CreatePersonDto { Name = "Miguel" };

            await _personServiceMock.Object.CreatePerson(createPersonDto);

            _personServiceMock.Verify(service => service.CreatePerson(createPersonDto), Times.Once);
        }

        [Fact]
        public async Task UpdatePersonAsync_ShouldCallServiceOnce()
        {
            var updatePersonDto = new UpdatePersonDto { Id = 1, Name = "Updated Miguel" };

            await _personServiceMock.Object.UpdatePersonAsync(1, updatePersonDto);

            _personServiceMock.Verify(service => service.UpdatePersonAsync(1, updatePersonDto), Times.Once);
        }

        [Fact]
        public async Task DeletePersonAsync_ShouldCallServiceOnce()
        {
            await _personServiceMock.Object.DeletePersonAsync(1);

            _personServiceMock.Verify(service => service.DeletePersonAsync(1), Times.Once);
        }

        [Fact]
        public async Task ConvertToDto_ShouldReturnPersonDtoAsync()
        {
            var person = Person.Create("Miguel Morales", "PS", "25669", DateTime.Now, "12345612000", "miguelmora@example.com", PersonType.Patient);
            var expectedDto = new PersonDto
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

            _personServiceMock.Setup(service => service.GetPersonById(person.Id)).ReturnsAsync(expectedDto);

            var result = await _personServiceMock.Object.GetPersonById(person.Id);

            Assert.NotNull(result);
            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal(expectedDto.Name, result.Name);
        }
    }
}