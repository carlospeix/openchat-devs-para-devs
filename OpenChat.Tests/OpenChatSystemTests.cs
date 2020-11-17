using OpenChat.Model;
using System;
using Xunit;

namespace OpenChat.Tests
{
    public class OpenChatSystemTests
    {
        [Fact]
        public void CanUserWithExistingUserName()
        {
            var system = new OpenChatSystem();

            _ = system.RegisterUser("Carlos", "irrelevant-1", "");

            var exception = Assert.Throws<InvalidOperationException>(
                () => system.RegisterUser("Carlos", "irrelevant-2", ""));

            Assert.Equal("Username already in use.", exception.Message);
            Assert.Equal(1, system.RegisterUsersCount());
        }
    }
}
