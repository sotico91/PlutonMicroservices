using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MSRecipes.Application.Interfaces;
using MSRecipes.Domain;
using Xunit;

namespace MSRecipes.Tests
{
	public class IRecipeRepositoryTests
    {
        private readonly Mock<IRecipeRepository> _recipeRepositoryMock;

        public IRecipeRepositoryTests()
        {
            _recipeRepositoryMock = new Mock<IRecipeRepository>();
        }

        [Fact]
        public async Task GetByIdAsync_WhenRecipeExists_ReturnsRecipe()
        {
            var recipeId = 1;
            var recipe = new Recipe { Id = recipeId, Code = "RCP123" };

            _recipeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(recipeId))
                .ReturnsAsync(recipe);

            var result = await _recipeRepositoryMock.Object.GetByIdAsync(recipeId);

            Assert.NotNull(result);
            Assert.Equal(recipeId, result.Id);
        }

        [Fact]
        public async Task GetByCodeAsync_WhenRecipeExists_ReturnsRecipe()
        {
            var recipeCode = "RCP123";
            var recipe = new Recipe { Id = 1, Code = recipeCode };

            _recipeRepositoryMock
                .Setup(repo => repo.GetByCodeAsync(recipeCode))
                .ReturnsAsync(recipe);

            var result = await _recipeRepositoryMock.Object.GetByCodeAsync(recipeCode);

            Assert.NotNull(result);
            Assert.Equal(recipeCode, result.Code);
        }

        [Fact]
        public async Task GetByPatientIdAsync_WhenRecipesExist_ReturnsRecipes()
        {
            var patientId = 123;
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, PatientId = patientId },
                new Recipe { Id = 2, PatientId = patientId }
            };

            _recipeRepositoryMock
                .Setup(repo => repo.GetByPatientIdAsync(patientId))
                .ReturnsAsync(recipes);

            var result = await _recipeRepositoryMock.Object.GetByPatientIdAsync(patientId);

            Assert.NotNull(result);
            Assert.Equal(2, ((List<Recipe>)result).Count);
        }

        [Fact]
        public async Task AddAsync_AddsRecipe()
        {
            var recipe = new Recipe { Id = 1, Code = "RCP123" };

            _recipeRepositoryMock.Setup(repo => repo.AddAsync(recipe))
                .Returns(Task.CompletedTask)
                .Verifiable();

            await _recipeRepositoryMock.Object.AddAsync(recipe);

            _recipeRepositoryMock.Verify(repo => repo.AddAsync(recipe), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesRecipe()
        {
            var recipe = new Recipe { Id = 1, Code = "RCP123" };

            _recipeRepositoryMock.Setup(repo => repo.UpdateAsync(recipe))
                .Returns(Task.CompletedTask)
                .Verifiable();

            await _recipeRepositoryMock.Object.UpdateAsync(recipe);

            _recipeRepositoryMock.Verify(repo => repo.UpdateAsync(recipe), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeletesRecipe()
        {
            var recipeId = 1;

            _recipeRepositoryMock.Setup(repo => repo.DeleteAsync(recipeId))
                .Returns(Task.CompletedTask)
                .Verifiable();

            await _recipeRepositoryMock.Object.DeleteAsync(recipeId);

            _recipeRepositoryMock.Verify(repo => repo.DeleteAsync(recipeId), Times.Once);
        }
    }
}