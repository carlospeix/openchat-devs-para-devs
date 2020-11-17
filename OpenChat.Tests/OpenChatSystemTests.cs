using OpenChat.Model;
using System;
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
    }
}
