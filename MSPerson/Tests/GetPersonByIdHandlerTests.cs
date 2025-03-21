using System.Threading.Tasks;
using System.Threading;
using Moq;
using MSPerson.Application.Handlers;
using MSPerson.Application.Queries;
using Xunit;
using System;
using MSPerson.Application.DTOs;
using MSPerson.Application.Interfaces;

namespace MSPerson.Tests
{
    public class GetPersonByIdHandlerTests
    {
        private readonly Mock<IPersonService> _serviceMock;
        private readonly GetPersonByIdHandler _handler;

        public GetPersonByIdHandlerTests()
        {
            _serviceMock = new Mock<IPersonService>();
            _handler = new GetPersonByIdHandler(_serviceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPersonDto_WhenPersonExists()
        {
            var personId = 1;
            var expectedPerson = new PersonDto
            {
                Id = personId,
                Name = "Nestur Alvarez",
                DocumentType = "PS",
                DocumentNumber = "12345678",
                DateOfBirth = DateTime.Now,
                PhoneNumber = "1234567890",
                Email = "email@example.com",
                PersonType = "Patient"
            };

            _serviceMock
                .Setup(service => service.GetPersonById(personId))
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

            _serviceMock
                .Setup(service => service.GetPersonById(personId))
                .ReturnsAsync((PersonDto)null);

            var query = new GetPersonByIdQuery(personId);
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }
    }
}