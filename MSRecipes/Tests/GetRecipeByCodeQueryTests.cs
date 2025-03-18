using MSRecipes.Application.Queries;
using Xunit;

namespace MSRecipes.Tests
{
	public class GetRecipeByCodeQueryTests
	{
        [Fact]
        public void Constructor_Should_Set_Code_Correctly()
        {
            var expectedCode = "RCP123";

            var query = new GetRecipeByCodeQuery(expectedCode);
            Assert.Equal(expectedCode, query.Code);
        }
    }
}