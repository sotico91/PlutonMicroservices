using System;
using System.Threading.Tasks;
using System.Threading;
using Moq;
using MSRecipes.Application.Handlers;
using MSRecipes.Application.Interfaces;
using MSRecipes.Application.Queries;
using MSRecipes.Domain;
using Xunit;

namespace MSRecipes.Tests
{
    public class GetRecipeByCodeHandlerTests
    {
        private readonly Mock<IRecipeRepository> _recipeRepositoryMock;
        private readonly GetRecipeByCodeHandler _handler;

        public GetRecipeByCodeHandlerTests()
        {
            _recipeRepositoryMock = new Mock<IRecipeRepository>();
            _handler = new GetRecipeByCodeHandler(_recipeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenRecipeExists_ReturnsRecipeDto()
        {
            // Arrange
            var recipeCode = "RCP123";
            var recipeEntity = new Recipe
            {
                Id = 1,
                Code = recipeCode,
                PatientId = 123,
                Description = "Sample Recipe",
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                Status = RecipeStatus.Active
            };

            _recipeRepositoryMock
                .Setup(repo => repo.GetByCodeAsync(recipeCode))
                .ReturnsAsync(recipeEntity);

            var query = new GetRecipeByCodeQuery(recipeCode);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(recipeEntity.Id, result.Id);
            Assert.Equal(recipeEntity.Code, result.Code);
            Assert.Equal(recipeEntity.PatientId, result.PatientId);
            Assert.Equal(recipeEntity.Description, result.Description);
            Assert.Equal(recipeEntity.CreatedDate, result.CreatedDate);
            Assert.Equal(recipeEntity.ExpiryDate, result.ExpiryDate);
            Assert.Equal(recipeEntity.Status.ToString(), result.Status);
        }

        [Fact]
        public async Task Handle_WhenRecipeDoesNotExist_ThrowsArgumentException()
        {
            var recipeCode = "NON_EXISTENT";
            _recipeRepositoryMock
                .Setup(repo => repo.GetByCodeAsync(recipeCode))
                .ReturnsAsync((Recipe)null);

            var query = new GetRecipeByCodeQuery(recipeCode);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _handler.Handle(query, CancellationToken.None));
        }
    }
}