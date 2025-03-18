using System;
using MSRecipes.Application.DTOs;
using Xunit;

namespace MSRecipes.Tests
{
	public class CreateRecipeDtoTests
	{
        [Fact]
        public void CreateRecipeDto_ShouldSetPropertiesCorrectly()
        {
 
            var code = "RCP123";
            var patientId = 1;
            var description = "Medicamento para la presión";
            var expiryDate = DateTime.UtcNow.AddDays(10);

          
            var dto = new CreateRecipeDto
            {
                Code = code,
                PatientId = patientId,
                Description = description,
                ExpiryDate = expiryDate
            };

            Assert.Equal(code, dto.Code);
            Assert.Equal(patientId, dto.PatientId);
            Assert.Equal(description, dto.Description);
            Assert.Equal(expiryDate, dto.ExpiryDate);
        }
    }
}