using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MSRecipes.Application.DTOs;
using MSRecipes.Application.Interfaces;
using MSRecipes.Application.Services;
using MSRecipes.Domain;
using Xunit;

namespace MSRecipes.Tests
{
	public class RecipeServiceTests
	{
        private readonly Mock<IRecipeRepository> _recipeRepositoryMock;
        private readonly RecipeService _recipeService;

        public RecipeServiceTests()
        {
            _recipeRepositoryMock = new Mock<IRecipeRepository>();
            _recipeService = new RecipeService(_recipeRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateRecipeAsync_ShouldAddRecipe()
        {
            var createRecipeDto = new CreateRecipeDto
            {
                Code = "RCP123",
                PatientId = 1,
                Description = "Test Recipe",
                ExpiryDate = DateTime.UtcNow.AddDays(30)
            };

            await _recipeService.CreateRecipeAsync(createRecipeDto);

            _recipeRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Recipe>()), Times.Once);
        }

        [Fact]
        public async Task UpdateRecipeStatusAsync_WhenRecipeExists_ShouldUpdateStatus()
        {
            var recipe = new Recipe
            {
                Id = 1,
                Code = "RCP123",
                Status = RecipeStatus.Active
            };

            var updateDto = new UpdateRecipeStatusDto { Status = "Expired" };

            _recipeRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(recipe);

            await _recipeService.UpdateRecipeStatusAsync(1, updateDto);

            _recipeRepositoryMock.Verify(r => r.UpdateAsync(recipe), Times.Once);
            Assert.Equal(RecipeStatus.Expired, recipe.Status);
        }

        [Fact]
        public async Task UpdateRecipeStatusAsync_WhenRecipeDoesNotExist_ShouldThrowException()
        {
            _recipeRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Recipe)null);

            var updateDto = new UpdateRecipeStatusDto { Status = "Expired" };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _recipeService.UpdateRecipeStatusAsync(1, updateDto));
        }

        [Fact]
        public async Task GetRecipeByCodeAsync_WhenRecipeExists_ReturnsRecipeDto()
        {
            var recipe = new Recipe
            {
                Id = 1,
                Code = "RCP123",
                PatientId = 1,
                Description = "Test Recipe",
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                Status = RecipeStatus.Active
            };

            _recipeRepositoryMock.Setup(r => r.GetByCodeAsync("RCP123")).ReturnsAsync(recipe);

            var result = await _recipeService.GetRecipeByCodeAsync("RCP123");

            Assert.NotNull(result);
            Assert.Equal(recipe.Id, result.Id);
            Assert.Equal(recipe.Code, result.Code);
            Assert.Equal(recipe.PatientId, result.PatientId);
        }

        [Fact]
        public async Task GetRecipeByCodeAsync_WhenRecipeDoesNotExist_ThrowsException()
        {
            _recipeRepositoryMock.Setup(r => r.GetByCodeAsync("NON_EXISTENT")).ReturnsAsync((Recipe)null);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _recipeService.GetRecipeByCodeAsync("NON_EXISTENT"));
        }

        [Fact]
        public async Task GetRecipesByPatientIdAsync_WhenRecipesExist_ReturnsList()
        {
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Code = "RCP1", PatientId = 1, Description = "Test 1", Status = RecipeStatus.Active },
                new Recipe { Id = 2, Code = "RCP2", PatientId = 1, Description = "Test 2", Status = RecipeStatus.Delivered }
            };

            _recipeRepositoryMock.Setup(r => r.GetByPatientIdAsync(1)).ReturnsAsync(recipes);

            var result = await _recipeService.GetRecipesByPatientIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}