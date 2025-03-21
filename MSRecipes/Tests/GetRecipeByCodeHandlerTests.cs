using System;
using System.Threading.Tasks;
using System.Threading;
using Moq;
using MSRecipes.Application.Handlers;
using MSRecipes.Application.Interfaces;
using MSRecipes.Application.Queries;
using Xunit;
using MSRecipes.Application.DTOs;

namespace MSRecipes.Tests
{
    public class GetRecipeByCodeHandlerTests
    {
        private readonly Mock<IRecipeService> _recipeServiceMock;
        private readonly GetRecipeByCodeHandler _handler;

        public GetRecipeByCodeHandlerTests()
        {
            _recipeServiceMock = new Mock<IRecipeService>();
            _handler = new GetRecipeByCodeHandler(_recipeServiceMock.Object);
        }

        [Fact]
        public async Task Handle_WhenRecipeExists_ReturnsRecipeDto()
        {
            // Arrange
            var recipeCode = "RCP123";
            var recipeDto = new RecipeDto
            {
                Id = 1,
                Code = recipeCode,
                PatientId = 123,
                Description = "Sample Recipe",
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                Status = "Active"
            };

            _recipeServiceMock
                .Setup(service => service.GetRecipeByCodeAsync(recipeCode))
                .ReturnsAsync(recipeDto);

            var query = new GetRecipeByCodeQuery(recipeCode);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(recipeDto.Id, result.Id);
            Assert.Equal(recipeDto.Code, result.Code);
            Assert.Equal(recipeDto.PatientId, result.PatientId);
            Assert.Equal(recipeDto.Description, result.Description);
            Assert.Equal(recipeDto.CreatedDate, result.CreatedDate);
            Assert.Equal(recipeDto.ExpiryDate, result.ExpiryDate);
            Assert.Equal(recipeDto.Status, result.Status);
        }

        [Fact]
        public async Task Handle_WhenRecipeDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            var recipeCode = "NON_EXISTENT";
            _recipeServiceMock
                .Setup(service => service.GetRecipeByCodeAsync(recipeCode))
                .ReturnsAsync((RecipeDto)null);

            var query = new GetRecipeByCodeQuery(recipeCode);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _handler.Handle(query, CancellationToken.None));
        }
    }
}