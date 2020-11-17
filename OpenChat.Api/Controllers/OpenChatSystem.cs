using System;

namespace OpenChat.Api.Controllers
{
    public class OpenChatSystem
    {
        public OpenChatSystem()
        {
        }

        public User RegisterUser(string name, string password, string about)
        {
            return User.Create(name, about);
        }
    }
}