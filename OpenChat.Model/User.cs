using System;

namespace OpenChat.Model
{
    public class User
    {
        public const string MSG_CAN_NOT_REGISTER_USER_WITH_EMPTY_NAME = "Can not register user with empty name.";

        public static User Create(string name, string about)
        {
            AssertNameIsNotEmpty(name);

            return new User(name, about);
        }

        private static void AssertNameIsNotEmpty(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new InvalidOperationException(MSG_CAN_NOT_REGISTER_USER_WITH_EMPTY_NAME);
        }

        public Guid Id { get; }
        public string Name { get; }
        public string About { get; }

        private User(string name, string about)
        {
            Id = Guid.NewGuid();
            Name = name;
            About = about;
        }

        internal bool IsNamed(string name)
        {
            return Name.Equals(name);
        }
    }
}