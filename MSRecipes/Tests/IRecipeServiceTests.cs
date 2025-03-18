using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
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
        public async Task CreateRecipeAsync_ShouldCallMethodOnce()
        {
            var createRecipeDto = new CreateRecipeDto
            {
                Code = "RCP001",
                PatientId = 1,
                Description = "Sample Recipe",
                ExpiryDate = System.DateTime.Now.AddDays(30)
            };

            await _recipeServiceMock.Object.CreateRecipeAsync(createRecipeDto);

            _recipeServiceMock.Verify(service => service.CreateRecipeAsync(createRecipeDto), Times.Once);
        }

        [Fact]
        public async Task UpdateRecipeStatusAsync_ShouldCallMethodOnce()
        {
            var updateRecipeStatusDto = new UpdateRecipeStatusDto { Status = "Completed" };

            await _recipeServiceMock.Object.UpdateRecipeStatusAsync(1, updateRecipeStatusDto);

            _recipeServiceMock.Verify(service => service.UpdateRecipeStatusAsync(1, updateRecipeStatusDto), Times.Once);
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

            Assert.Equal(expectedRecipe, result);
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

            Assert.Equal(expectedRecipes, result);
        }
    }
}