using System;
using MSPerson.Application.DTOs;
using Xunit;

namespace MSPerson.Tests
{
    public class PersonDtoTests
    {
        [Fact]
        public void PersonDto_ShouldSetPropertiesCorrectly()
        {
            var id = 1;
            var name = "Nestur Alvarez";
            var documentType = "PS";
            var documentNumber = "566999";
            var dateOfBirth = new DateTime(1988, 04, 20);
            var phoneNumber = "12345614454";
            var email = "nestur.alvarez@example.com";
            var personType = "Patient";

            var personDto = new PersonDto
            {
                Id = id,
                Name = name,
                DocumentType = documentType,
                DocumentNumber = documentNumber,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                Email = email,
                PersonType = personType
            };

            Assert.Equal(id, personDto.Id);
            Assert.Equal(name, personDto.Name);
            Assert.Equal(documentType, personDto.DocumentType);
            Assert.Equal(documentNumber, personDto.DocumentNumber);
            Assert.Equal(dateOfBirth, personDto.DateOfBirth);
            Assert.Equal(phoneNumber, personDto.PhoneNumber);
            Assert.Equal(email, personDto.Email);
            Assert.Equal(personType, personDto.PersonType);
        }
    }
}
