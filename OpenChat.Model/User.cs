using System;

namespace OpenChat.Model
{
    public class User
    {
        public static User Create(string name, string about)
        {
            if (String.IsNullOrEmpty(name))
                throw new InvalidOperationException("Can not register user with empty name.");

            return new User(name, about);
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
    }
}