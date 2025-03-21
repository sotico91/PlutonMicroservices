using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MSRecipes.Application.Commands;
using MSRecipes.Application.DTOs;
using MSRecipes.Application.Interfaces;
using Xunit;

namespace MSRecipes.Tests
{
	public class IRecipeServiceTests
    {
        private readonly Mock<IRecipeService> _recipeServiceMock;

        public IRecipeServiceTests()
        {
            _recipeServiceMock = new Mock<IRecipeService>();
        }

        [Fact]
        public async Task CreateRecipeAsync_ShouldReturnRecipeId()
        {
  
            var createRecipeCommand = new CreateRecipeCommand
            {
                Code = "RCP001",
                PatientId = 1,
                Description = "Sample Recipe",
                ExpiryDate = System.DateTime.Now.AddDays(30)
            };

            _recipeServiceMock.Setup(service => service.CreateRecipeAsync(createRecipeCommand))
                .ReturnsAsync(1);

            var result = await _recipeServiceMock.Object.CreateRecipeAsync(createRecipeCommand);

            Assert.Equal(1, result);
            _recipeServiceMock.Verify(service => service.CreateRecipeAsync(createRecipeCommand), Times.Once);
        }

        [Fact]
        public async Task UpdateRecipeStatusAsync_ShouldCallMethodOnce()
        {
            
            var updateRecipeCommand = new UpdateRecipeCommand
            {
                Id = 1,
                Status = "Completed"
            };

            _recipeServiceMock.Setup(service => service.UpdateRecipeStatusAsync(updateRecipeCommand))
                .Returns(Task.CompletedTask);

            await _recipeServiceMock.Object.UpdateRecipeStatusAsync(updateRecipeCommand);

            _recipeServiceMock.Verify(service => service.UpdateRecipeStatusAsync(updateRecipeCommand), Times.Once);
        }

        [Fact]
        public async Task GetRecipeByCodeAsync_ShouldReturnRecipeDto()
        {

            var recipeCode = "RCP001";
            var expectedRecipe = new RecipeDto
            {
                Id = 1,
                Code = recipeCode,
                PatientId = 1,
                Description = "Sample Recipe",
                CreatedDate = System.DateTime.Now,
                ExpiryDate = System.DateTime.Now.AddDays(30),
                Status = "Active"
            };

            _recipeServiceMock.Setup(service => service.GetRecipeByCodeAsync(recipeCode))
                .ReturnsAsync(expectedRecipe);

            var result = await _recipeServiceMock.Object.GetRecipeByCodeAsync(recipeCode);

            Assert.NotNull(result);
            Assert.Equal(expectedRecipe.Id, result.Id);
            Assert.Equal(expectedRecipe.Code, result.Code);
            Assert.Equal(expectedRecipe.PatientId, result.PatientId);
            Assert.Equal(expectedRecipe.Description, result.Description);
            Assert.Equal(expectedRecipe.CreatedDate, result.CreatedDate);
            Assert.Equal(expectedRecipe.ExpiryDate, result.ExpiryDate);
            Assert.Equal(expectedRecipe.Status, result.Status);
        }

        [Fact]
        public async Task GetRecipesByPatientIdAsync_ShouldReturnListOfRecipes()
        {
            var patientId = 1;
            var expectedRecipes = new List<RecipeDto>
            {
                new RecipeDto { Id = 1, Code = "RCP001", PatientId = patientId, Description = "Recipe 1", Status = "Active" },
                new RecipeDto { Id = 2, Code = "RCP002", PatientId = patientId, Description = "Recipe 2", Status = "Pending" }
            };

            _recipeServiceMock.Setup(service => service.GetRecipesByPatientIdAsync(patientId))
                .ReturnsAsync(expectedRecipes);

            var result = await _recipeServiceMock.Object.GetRecipesByPatientIdAsync(patientId);

            Assert.NotNull(result);
            Assert.Equal(expectedRecipes.Count, result.Count());
            _recipeServiceMock.Verify(service => service.GetRecipesByPatientIdAsync(patientId), Times.Once);
        }
    }
}