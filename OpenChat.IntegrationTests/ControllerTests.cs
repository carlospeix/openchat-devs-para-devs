using System.Net;
using OpenChat.Api.Controllers;
using OpenChat.Model;
using Xunit;

namespace OpenChat.IntegrationTests
{
    public class ControllerTests
    {
        private readonly DefaultController controller;

        public ControllerTests()
        {
            controller = new DefaultController(new OpenChatSystem(), null);
        }

        // Register New User
        // POST - openchat/registration { "username" : "Alice", "password" : "alki324d", "about" : "I love playing the piano and travelling." }
        // Failure Status: BAD_REQUEST - 400 Response: "Username already in use."
        [Fact]
        public void Registration_UserWithEmptyMailFails()
        {
            // Act
            var result = controller.RegisterUser(new RegistrationRequest("", "irrelevant", "irrelevant"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            Assert.Equal(User.MSG_CAN_NOT_REGISTER_USER_WITH_EMPTY_NAME, (string)result.Value);
        }
    }
}
