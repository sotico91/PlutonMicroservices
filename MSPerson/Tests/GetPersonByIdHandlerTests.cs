using System.Threading.Tasks;
using System.Threading;
using Moq;
using MSPerson.Application.Handlers;
using MSPerson.Application.interfaces;
using MSPerson.Application.Queries;
using MSPerson.Domain;
using Xunit;
using System;

namespace MSPerson.Tests
{
	public class GetPersonByIdHandlerTests
	{
        private readonly Mock<IPersonRepository> _repositoryMock;
        private readonly GetPersonByIdHandler _handler;

        public GetPersonByIdHandlerTests()
        {
            _repositoryMock = new Mock<IPersonRepository>();
            _handler = new GetPersonByIdHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPerson_WhenPersonExists()
        {
            var personId = 1;
            var expectedPerson = Person.Create("Nestur Alvarez", "PS", "12345678", DateTime.Now, "1234567890", "email@example.com", PersonType.Patient);

            _repositoryMock
                .Setup(repo => repo.GetByIdAsync(personId))
                .ReturnsAsync(expectedPerson);

            var query = new GetPersonByIdQuery(personId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(expectedPerson.Id, result.Id);
            Assert.Equal(expectedPerson.Name, result.Name);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenPersonDoesNotExist()
        {
            var personId = 1;

            _repositoryMock
                .Setup(repo => repo.GetByIdAsync(personId))
                .ReturnsAsync((Person)null);

            var query = new GetPersonByIdQuery(personId);
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }
    }
}