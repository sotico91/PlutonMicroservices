using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Moq;
using MSRecipes.Application.Commands;
using MSRecipes.Application.DTOs;
using MSRecipes.Application.Queries;
using MSRecipes.Application.Services;
using MSRecipes.Domain;
using Xunit;

namespace MSRecipes.Tests
{
	public class RecipeServiceTests
	{
        private readonly Mock<IMediator> _mediatorMock;
        private readonly RecipeService _recipeService;

        public RecipeServiceTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _recipeService = new RecipeService(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateRecipeAsync_ShouldSendCreateRecipeCommand()
        {
            var createRecipeDto = new CreateRecipeDto
            {
                Code = "RCP123",
                PatientId = 1,
                Description = "Test Recipe",
                ExpiryDate = DateTime.UtcNow.AddDays(30)
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateRecipeCommand>(), default)).ReturnsAsync(1);

            var result = await _recipeService.CreateRecipeAsync(createRecipeDto);

            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateRecipeCommand>(), default), Times.Once);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task UpdateRecipeStatusAsync_ShouldSendUpdateRecipeCommand()
        {
            var updateDto = new UpdateRecipeStatusDto { Status = RecipeStatus.Expired.ToString() };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateRecipeCommand>(), default)).ReturnsAsync(true);

            await _recipeService.UpdateRecipeStatusAsync(1, updateDto);

            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateRecipeCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task UpdateRecipeStatusAsync_WhenInvalidStatus_ShouldThrowException()
        {
            var updateDto = new UpdateRecipeStatusDto { Status = "InvalidStatus" };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _recipeService.UpdateRecipeStatusAsync(1, updateDto));
        }

        [Fact]
        public async Task GetRecipeByCodeAsync_ShouldSendGetRecipeByCodeQuery()
        {
            var recipeDto = new RecipeDto { Id = 1, Code = "RCP123", PatientId = 1 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRecipeByCodeQuery>(), default)).ReturnsAsync(recipeDto);

            var result = await _recipeService.GetRecipeByCodeAsync("RCP123");

            _mediatorMock.Verify(m => m.Send(It.IsAny<GetRecipeByCodeQuery>(), default), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(recipeDto.Id, result.Id);
        }

        [Fact]
        public async Task GetRecipesByPatientIdAsync_ShouldSendGetRecipesByPatientIdQuery()
        {
            var recipes = new List<RecipeDto>
            {
                new RecipeDto { Id = 1, Code = "RCP1", PatientId = 1 },
                new RecipeDto { Id = 2, Code = "RCP2", PatientId = 1 }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRecipesByPatientIdQuery>(), default)).ReturnsAsync(recipes);

            var result = await _recipeService.GetRecipesByPatientIdAsync(1);

            _mediatorMock.Verify(m => m.Send(It.IsAny<GetRecipesByPatientIdQuery>(), default), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(recipes.Count, result.Count());
        }
    }
}