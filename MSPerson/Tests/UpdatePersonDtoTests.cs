using System;
using MSPerson.Application.DTOs;
using Xunit;

namespace MSPerson.Tests
{
	public class UpdatePersonDtoTests
	{
        [Fact]
        public void UpdatePersonDto_ShouldSetPropertiesCorrectly()
        {
            var dto = new UpdatePersonDto
            {
                Id = 1,
                Name = "Andres Alba",
                DocumentType = "PS",
                DocumentNumber = "12345678",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "1234567890",
                Email = "aalba@example.com",
                PersonType = "Doctor"
            };

            Assert.Equal(1, dto.Id);
            Assert.Equal("Andres Alba", dto.Name);
            Assert.Equal("PS", dto.DocumentType);
            Assert.Equal("12345678", dto.DocumentNumber);
            Assert.Equal(new DateTime(1990, 1, 1), dto.DateOfBirth);
            Assert.Equal("1234567890", dto.PhoneNumber);
            Assert.Equal("aalba@example.com", dto.Email);
            Assert.Equal("Doctor", dto.PersonType);
        }
    }
}