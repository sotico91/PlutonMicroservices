using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MSPerson.Application.interfaces;
using MSPerson.Domain;
using Xunit;

namespace MSPerson.Tests
{
	public class IPersonRepositoryTests
	{
        private readonly Mock<IPersonRepository> _repositoryMock;

        public IPersonRepositoryTests()
        {
            _repositoryMock = new Mock<IPersonRepository>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPerson_WhenPersonExists()
        {
            var personId = 1;
            var expectedPerson = Person.Create("Nestur Alvarez", "PS", "12345678", DateTime.Now, "1234567890", "email@example.com", PersonType.Patient);

            _repositoryMock
                .Setup(repo => repo.GetByIdAsync(personId))
                .ReturnsAsync(expectedPerson);

            var result = await _repositoryMock.Object.GetByIdAsync(personId);

            Assert.NotNull(result);
            Assert.Equal(expectedPerson.Id, result.Id);
            Assert.Equal(expectedPerson.Name, result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenPersonDoesNotExist()
        {

            var personId = 999;

            _repositoryMock
                .Setup(repo => repo.GetByIdAsync(personId))
                .ReturnsAsync((Person)null);
            var result = await _repositoryMock.Object.GetByIdAsync(personId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfPersons()
        {
            var persons = new List<Person>
            {
               Person.Create("Nestur Alvarez", "PS", "12345678", DateTime.Now, "1234567890", "nesturAlva@example.com", PersonType.Patient),
              Person.Create("Andrea Blanco", "PS", "123411478", DateTime.Now, "1234566690", "Andreblanco@example.com", PersonType.Doctor)
            };

            _repositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(persons);

            var result = await _repositoryMock.Object.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepositoryOnce()
        {
            var person = Person.Create("Carlos Mina", "PS", "54233669", DateTime.Now, "5578899", "carlosMina@example.com", PersonType.Patient);

            _repositoryMock
                .Setup(repo => repo.AddAsync(person))
                .Returns(Task.CompletedTask);
            await _repositoryMock.Object.AddAsync(person);
            _repositoryMock.Verify(repo => repo.AddAsync(person), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryOnce()
        {
            var person = Person.Create("Carlos Mina", "PS", "54233669", DateTime.Now, "5578899", "carlosMina@example.com", PersonType.Patient);

            _repositoryMock
                .Setup(repo => repo.UpdateAsync(person))
                .Returns(Task.CompletedTask);

            await _repositoryMock.Object.UpdateAsync(person);

            _repositoryMock.Verify(repo => repo.UpdateAsync(person), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryOnce()
        {
            var personId = 1;

            _repositoryMock
                .Setup(repo => repo.DeleteAsync(personId))
                .Returns(Task.CompletedTask);

            await _repositoryMock.Object.DeleteAsync(personId);

            _repositoryMock.Verify(repo => repo.DeleteAsync(personId), Times.Once);
        }
    }
}