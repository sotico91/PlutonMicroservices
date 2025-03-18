using System;
using MSRecipes.Application.DTOs;
using Xunit;

namespace MSRecipes.Tests
{
    public class RecipeDtoTests
    {
        [Fact]
        public void RecipeDto_ShouldSetPropertiesCorrectly()
        {

            var id = 1;
            var code = "RX001";
            var patientId = 123;
            var description = "Pain reliever";
            var createdDate = DateTime.UtcNow;
            var expiryDate = DateTime.UtcNow.AddDays(30);
            var status = "Active";


            var dto = new RecipeDto
            {
                Id = id,
                Code = code,
                PatientId = patientId,
                Description = description,
                CreatedDate = createdDate,
                ExpiryDate = expiryDate,
                Status = status
            };


            Assert.Equal(id, dto.Id);
            Assert.Equal(code, dto.Code);
            Assert.Equal(patientId, dto.PatientId);
            Assert.Equal(description, dto.Description);
            Assert.Equal(createdDate, dto.CreatedDate);
            Assert.Equal(expiryDate, dto.ExpiryDate);
            Assert.Equal(status, dto.Status);
        }
    }
}