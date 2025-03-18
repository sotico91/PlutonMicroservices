using MSRecipes.Application.DTOs;
using Xunit;

namespace MSRecipes.Tests
{
	public class UpdateRecipeStatusDtoTests
    {
        [Fact]
        public void UpdateRecipeStatusDto_ShouldSetStatusCorrectly()
        {
            var status = "Completed";

            var dto = new UpdateRecipeStatusDto { Status = status };
            Assert.Equal(status, dto.Status);
        }
    }
}