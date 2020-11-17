using System;

namespace OpenChat.Model
{
    internal class Credential
    {
        internal static Credential Create(string password)
        {
            return new Credential(password);
        }

        private readonly string password;

        private Credential(string password)
        {
            this.password = password;
        }

        internal bool WithPassword(string password)
        {
            return this.password.Equals(password);
        }
    }
}