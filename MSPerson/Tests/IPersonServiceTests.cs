using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        public void ConvertToDto_ShouldReturnPersonDto()
        {
            var person = new Person { Id = 1, Name = "Miguel" };
            var expectedDto = new PersonDto { Id = 1, Name = "Miguel" };

            _personServiceMock.Setup(service => service.ConvertToDto(person)).Returns(expectedDto);

            var result = _personServiceMock.Object.ConvertToDto(person);

            Assert.NotNull(result);
            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal(expectedDto.Name, result.Name);
        }
    }
}