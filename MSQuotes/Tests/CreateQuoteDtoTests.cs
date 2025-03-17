using System;
using MSQuotes.Application.DTOs;
using Xunit;

namespace MSQuotes.Tests
{
	public class CreateQuoteDtoTests
    {
        [Fact]
        public void CreateQuoteDto_ShouldSetPropertiesCorrectly()
        {
            var date = DateTime.Now;
            var location = "Clinic A";
            var patientId = 1;
            var doctorId = 2;

            var createQuoteDto = new CreateQuoteDto
            {
                Date = date,
                Location = location,
                PatientId = patientId,
                DoctorId = doctorId
            };

            Assert.Equal(date, createQuoteDto.Date);
            Assert.Equal(location, createQuoteDto.Location);
            Assert.Equal(patientId, createQuoteDto.PatientId);
            Assert.Equal(doctorId, createQuoteDto.DoctorId);
        }

        [Fact]
        public void CreateQuoteDto_ShouldHandleEmptyLocation()
        {
            var createQuoteDto = new CreateQuoteDto
            {
                Date = DateTime.Now,
                Location = string.Empty,
                PatientId = 1,
                DoctorId = 2
            };

            Assert.Empty(createQuoteDto.Location);
        }

        [Fact]
        public void CreateQuoteDto_ShouldHandleNegativeIds()
        {
            var createQuoteDto = new CreateQuoteDto
            {
                Date = DateTime.Now,
                Location = "Clinic B",
                PatientId = -1,
                DoctorId = -2
            };

            Assert.Equal(-1, createQuoteDto.PatientId);
            Assert.Equal(-2, createQuoteDto.DoctorId);
        }
    }
}