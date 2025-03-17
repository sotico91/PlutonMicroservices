using System;
using MSQuotes.Application.DTOs;
using Xunit;

namespace MSQuotes.Tests
{
	public class UpdateQuoteStatusDtoTests
	{
        [Fact]
        public void UpdateQuoteStatusDto_ShouldAssignPropertiesCorrectly()
        {
            var updateQuoteStatusDto = new UpdateQuoteStatusDto
            {
                Code = "Q12345",
                Status = "Completed",
                Description = "Quote Completed successfully",
                ExpiryDate = new DateTime(2025, 12, 31)
            };

            Assert.Equal("Q12345", updateQuoteStatusDto.Code);
            Assert.Equal("Completed", updateQuoteStatusDto.Status);
            Assert.Equal("Quote Completed successfully", updateQuoteStatusDto.Description);
            Assert.Equal(new DateTime(2025, 12, 31), updateQuoteStatusDto.ExpiryDate);
        }

        [Fact]
        public void UpdateQuoteStatusDto_ShouldHandleEmptyDescription()
        {
            var updateQuoteStatusDto = new UpdateQuoteStatusDto
            {
                Code = "Q12345",
                Status = "In Progress",
                Description = string.Empty,
                ExpiryDate = new DateTime(2025, 06, 15)
            };

            Assert.Equal(string.Empty, updateQuoteStatusDto.Description);
        }

        [Fact]
        public void UpdateQuoteStatusDto_ShouldAcceptPastExpiryDate()
        {
            var updateQuoteStatusDto = new UpdateQuoteStatusDto
            {
                Code = "Q12345",
                Status = "Pending",
                Description = "Quote Pending",
                ExpiryDate = new DateTime(2020, 01, 01)
            };

            Assert.Equal(new DateTime(2020, 01, 01), updateQuoteStatusDto.ExpiryDate);
        }
    }
}