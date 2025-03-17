using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MSAuthServ.Domain;
using Xunit;

namespace MSAuthServ.Tests
{
	public class LoginRequestTests
    {
        [Fact]
        public void LoginRequest_ShouldAssignValues_Correctly()
        {
            // Arrange
            var username = "testuser";
            var password = "TestPassword123";

            // Act
            var loginRequest = new LoginRequest
            {
                Username = username,
                Password = password
            };

            // Assert
            Assert.Equal(username, loginRequest.Username);
            Assert.Equal(password, loginRequest.Password);
        }
    }
}