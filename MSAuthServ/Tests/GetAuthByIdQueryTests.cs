using MSAuthServ.Application.Queries;
using Xunit;

namespace MSAuthServ.Tests
{
	public class GetAuthByIdQueryTests
    {
        [Fact]
        public void Constructor_ShouldSetUserIdAndIncludeRoles()
        {
            // Arrange
            var userId = 1;
            var includeRoles = true;

            // Act
            var query = new GetAuthByIdQuery(userId, includeRoles);

            // Assert
            Assert.Equal(userId, query.UserId);
            Assert.True(query.IncludeRoles);
        }

        [Fact]
        public void Constructor_ShouldSetDefaultIncludeRolesToFalse()
        {
            // Arrange
            var userId = 1;

            // Act
            var query = new GetAuthByIdQuery(userId);

            // Assert
            Assert.Equal(userId, query.UserId);
            Assert.False(query.IncludeRoles);
        }
    }
}