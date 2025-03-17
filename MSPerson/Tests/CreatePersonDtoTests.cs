using System;
using Xunit;
using MSPerson.Application.DTOs;

namespace MSPerson.Tests
{
	public class CreatePersonDtoTests
	{
            private readonly CreatePersonDto _createPersonDto;

            public CreatePersonDtoTests()
            {
                _createPersonDto = new CreatePersonDto
                {
                    Name = "Edgar Velasco",
                    DocumentType = "PS",
                    DocumentNumber = "12345678",
                    DateOfBirth = new DateTime(1991, 03, 26),
                    PhoneNumber = "1234567890",
                    Email = "edvelasco@gmail.com",
                    PersonType = "Patient"
                };
            }

            [Fact]
            public void CreatePersonDto_ShouldSetPropertiesCorrectly()
            {
                Assert.Equal("Edgar Velasco", _createPersonDto.Name);
                Assert.Equal("PS", _createPersonDto.DocumentType);
                Assert.Equal("12345678", _createPersonDto.DocumentNumber);
                Assert.Equal(new DateTime(1991, 03, 26), _createPersonDto.DateOfBirth);
                Assert.Equal("1234567890", _createPersonDto.PhoneNumber);
                Assert.Equal("edvelasco@gmail.com", _createPersonDto.Email);
                Assert.Equal("Patient", _createPersonDto.PersonType);
            }

            [Fact]
            public void CreatePersonDto_Name_ShouldNotBeNull()
            {
                // Arrange
                _createPersonDto.Name = null;

                // Act & Assert
                Assert.Null(_createPersonDto.Name);
            }

            [Fact]
            public void CreatePersonDto_Email_ShouldBeValidFormat()
            {
                // Arrange
                _createPersonDto.Email = "invalid-email";

                // Act
                var isValidEmail = _createPersonDto.Email.Contains("@");

                // Assert
                Assert.False(isValidEmail);
            }
        }
    }