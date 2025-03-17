using MSAuthServ.Domain;
using Xunit;

namespace MSAuthServ.Tests
{
	public class UserTests
	{
        [Fact]
        public void User_ShouldAssignValues_Correctly()
        {
            // Arrange
            var userId = 1;
            var username = "TestUser";
            var password = "SecurePassword";

            // Act
            var user = new User
            {
                Id = userId,
                Username = username,
                Password = password
            };

            // Assert
            Assert.Equal(userId, user.Id);
            Assert.Equal(username, user.Username);
            Assert.Equal(password, user.Password);
        }
    }
}