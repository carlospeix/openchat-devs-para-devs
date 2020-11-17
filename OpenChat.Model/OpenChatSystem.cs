using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenChat.Model
{
    public class OpenChatSystem
    {
        public const string MSG_USER_NAME_ALREADY_IN_USE = "Username already in use.";

        private readonly IList<User> registeredUsers;

        public OpenChatSystem()
        {
            registeredUsers = new List<User>();
        }

        public User RegisterUser(string name, string password, string about)
        {
            AssertNewUserNameIsNotRegistered(name);

            var user = User.Create(name, about);
            registeredUsers.Add(user);

            return user;
        }

        public int RegisterUsersCount()
        {
            return registeredUsers.Count();
        }

        private void AssertNewUserNameIsNotRegistered(string userName)
        {
            if (registeredUsers.Any(user => user.IsNamed(userName)))
                throw new InvalidOperationException(MSG_USER_NAME_ALREADY_IN_USE);
        }
    }
}