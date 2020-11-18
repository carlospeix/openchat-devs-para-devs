using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenChat.Model
{
    public class OpenChatSystem
    {
        public const string MSG_USER_NAME_ALREADY_IN_USE = "Username already in use.";
        public const string MSG_INVALID_CREDENTIALS = "Invalid credentials.";

        private readonly IList<User> registeredUsers;
        private readonly IDictionary<User, Credential> registeredCredentials;

        public OpenChatSystem()
        {
            registeredUsers = new List<User>();
            registeredCredentials = new Dictionary<User, Credential>();
        }

        public User RegisterUser(string name, string password, string about)
        {
            AssertNewUserNameIsNotRegistered(name);

            var user = User.Create(name, about);
            registeredUsers.Add(user);
            
            var credential = Credential.Create(password);
            registeredCredentials.Add(user, credential);

            return user;
        }

        public int RegisterUsersCount()
        {
            return registeredUsers.Count();
        }

        private void AssertNewUserNameIsNotRegistered(string name)
        {
            if (registeredUsers.Any(user => user.IsNamed(name)))
                throw new InvalidOperationException(MSG_USER_NAME_ALREADY_IN_USE);
        }

        public User LoginUser(string userName, string password)
        {
            var user = registeredCredentials.SingleOrDefault(
                (kvp) => kvp.Key.IsNamed(userName) && kvp.Value.WithPassword(password)).Key;

            if (user == default(User))
                throw new InvalidOperationException(MSG_INVALID_CREDENTIALS);

            return user;
        }
    }
}