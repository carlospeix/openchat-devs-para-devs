using System;
using OpenChat.Model;
using Xunit;

namespace OpenChat.Tests
{
    public class OpenChatSystemTests
    {
        private readonly OpenChatSystem system;

        public OpenChatSystemTests()
        {
            system = new OpenChatSystem();
        }

        [Fact]
        public void CanNotUserWithExistingUserName()
        {
            _ = system.RegisterUser("Carlos", "irrelevant-1", "");

            var exception = Assert.Throws<InvalidOperationException>(
                () => system.RegisterUser("Carlos", "irrelevant-2", ""));

            Assert.Equal(OpenChatSystem.MSG_USER_NAME_ALREADY_IN_USE, exception.Message);
            Assert.Equal(1, system.RegisterUsersCount());
        }

        // CanNotLoginWithWrongUserName
        [Fact]
        public void CanNotLoginWithWrongUserName()
        {
            _ = system.RegisterUser("Carlos", "Pass0rd!", "");

            var exception = Assert.Throws<InvalidOperationException>(
                () => system.LoginUser("WRONG", "Pass0rd!"));

            Assert.Equal(OpenChatSystem.MSG_INVALID_CREDENTIALS, exception.Message);
        }

        [Fact]
        public void User_CanNotRegisterUserWithEmptyPassword()
        {
            var exception = Assert.Throws<InvalidOperationException>(
                () => system.RegisterUser("irrelevant", "", ""));

            Assert.Equal(Credential.MSG_CANT_CREATE_CREDENTIAL_WITH_EMPTY_PASSWORD, exception.Message);
        }
    }
}
