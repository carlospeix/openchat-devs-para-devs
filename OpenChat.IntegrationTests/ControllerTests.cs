﻿using System.Net;
using OpenChat.Api.Controllers;
using Xunit;

namespace OpenChat.IntegrationTests
{
    public class ControllerTests
    {
        public ControllerTests()
        {
        }

        // Register New User
        // POST - openchat/registration { "username" : "Alice", "password" : "alki324d", "about" : "I love playing the piano and travelling." }
        // Failure Status: BAD_REQUEST - 400 Response: "Username already in use."
        [Fact]
        public void Registration_UserWithEmptyMailFails()
        {
            var controller = new DefaultController();

            // Act
            var result = controller.RegisterUser(new RegistrationRequest("", "irrelevant", "irrelevant"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            Assert.Equal("Can not register user with empty name.", (string)result.Value);
        }
    }
}
