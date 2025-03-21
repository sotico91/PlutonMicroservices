using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MSRecipes.Application.Commands;
using MSRecipes.Application.Interfaces;
using MSRecipes.Application.Services;
using MSRecipes.Domain;
using Xunit;

namespace MSRecipes.Tests
{
	public class RecipeServiceTests
	{
        private readonly Mock<IRecipeRepository> _repositoryMock;
        private readonly RecipeService _recipeService;

        public RecipeServiceTests()
        {
            _repositoryMock = new Mock<IRecipeRepository>();
            _recipeService = new RecipeService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateRecipeAsync_ShouldReturnRecipeId()
        {
            var createRecipeCommand = new CreateRecipeCommand
            {
                Code = "RCP123",
                PatientId = 1,
                Description = "Test Recipe",
                ExpiryDate = DateTime.UtcNow.AddDays(30)
            };

            var recipe = new Recipe
            {
                Id = 1,
                Code = createRecipeCommand.Code,
                PatientId = createRecipeCommand.PatientId,
                Description = createRecipeCommand.Description,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = createRecipeCommand.ExpiryDate,
                Status = RecipeStatus.Active
            };

            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Recipe>()))
                .Callback<Recipe>(r => r.Id = recipe.Id) 
                .Returns(Task.CompletedTask);

            var result = await _recipeService.CreateRecipeAsync(createRecipeCommand);

            Assert.Equal(1, result);
            _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Recipe>()), Times.Once);
        }

        [Fact]
        public async Task UpdateRecipeStatusAsync_ShouldUpdateRecipe()
        {

            var updateRecipeCommand = new UpdateRecipeCommand { Id = 1, Status = "Delivered" };
            var recipe = new Recipe { Id = 1, Code = "RCP123", Status = RecipeStatus.Active };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(recipe);
            _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Recipe>())).Returns(Task.CompletedTask);

            await _recipeService.UpdateRecipeStatusAsync(updateRecipeCommand);

            _repositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Recipe>()), Times.Once);
            Assert.Equal(RecipeStatus.Delivered, recipe.Status);
        }

        [Fact]
        public async Task UpdateRecipeStatusAsync_WhenInvalidStatus_ShouldThrowException()
        {
            var updateRecipeCommand = new UpdateRecipeCommand { Id = 1, Status = "InvalidStatus" };

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _recipeService.UpdateRecipeStatusAsync(updateRecipeCommand));
        }

        [Fact]
        public async Task UpdateRecipeStatusAsync_WhenRecipeNotFound_ShouldThrowKeyNotFoundException()
        {

            var updateRecipeCommand = new UpdateRecipeCommand { Id = 1, Status = "Active" };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Recipe)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _recipeService.UpdateRecipeStatusAsync(updateRecipeCommand));
        }

        [Fact]
        public async Task GetRecipeByCodeAsync_ShouldReturnRecipeDto()
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

            _repositoryMock.Setup(repo => repo.GetByCodeAsync("RCP123")).ReturnsAsync(recipe);

            var result = await _recipeService.GetRecipeByCodeAsync("RCP123");

            Assert.NotNull(result);
            Assert.Equal(recipe.Id, result.Id);
            Assert.Equal(recipe.Code, result.Code);
            _repositoryMock.Verify(repo => repo.GetByCodeAsync("RCP123"), Times.Once);
        }

        [Fact]
        public async Task GetRecipesByPatientIdAsync_ShouldReturnListOfRecipes()
        {
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Code = "RCP1", PatientId = 1 },
                new Recipe { Id = 2, Code = "RCP2", PatientId = 1 }
            };

            _repositoryMock.Setup(repo => repo.GetByPatientIdAsync(1)).ReturnsAsync(recipes);

            var result = await _recipeService.GetRecipesByPatientIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(recipes.Count, result.Count());
            _repositoryMock.Verify(repo => repo.GetByPatientIdAsync(1), Times.Once);
        }
    }
}