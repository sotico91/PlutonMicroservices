using System;
using MSQuotes.Application.DTOs;
using Xunit;

namespace MSQuotes.Application.Services
{
	public class QuoteDtoTests
	{
        [Fact]
        public void QuoteDto_ShouldAssignPropertiesCorrectly()
        {
            var quoteDto = new QuoteDto
            {
                Id = 1,
                Date = new DateTime(2025, 03, 17),
                Location = "Clinic A",
                PatientId = 101,
                DoctorId = 202,
                Status = "Pending"
            };

            Assert.Equal(1, quoteDto.Id);
            Assert.Equal(new DateTime(2025, 03, 17), quoteDto.Date);
            Assert.Equal("Clinic A", quoteDto.Location);
            Assert.Equal(101, quoteDto.PatientId);
            Assert.Equal(202, quoteDto.DoctorId);
            Assert.Equal("Pending", quoteDto.Status);
        }

        [Fact]
        public void QuoteDto_ShouldHandleEmptyLocation()
        {
            var quoteDto = new QuoteDto
            {
                Id = 2,
                Date = DateTime.Now,
                Location = string.Empty,
                PatientId = 103,
                DoctorId = 204,
                Status = "Completed"
            };

            Assert.Equal(string.Empty, quoteDto.Location);
        }

        [Fact]
        public void QuoteDto_ShouldAllowNegativeIds()
        {
            var quoteDto = new QuoteDto
            {
                Id = -1,
                Date = DateTime.Now,
                Location = "Clinic B",
                PatientId = -105,
                DoctorId = -206,
                Status = "Cancelled"
            };

            Assert.Equal(-1, quoteDto.Id);
            Assert.Equal(-105, quoteDto.PatientId);
            Assert.Equal(-206, quoteDto.DoctorId);
        }
    }
}