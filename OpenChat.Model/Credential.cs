﻿using System;

namespace OpenChat.Model
{
    public class Credential
    {
        public const string MSG_CANT_CREATE_CREDENTIAL_WITH_EMPTY_PASSWORD = "Can't create credential with empty password.";

        internal static Credential Create(string password)
        {
            AssertPasswordIsNotEmpty(password);
            
            return new Credential(password);
        }

        private static void AssertPasswordIsNotEmpty(string password)
        {
            if (String.IsNullOrWhiteSpace(password))
                throw new InvalidOperationException(MSG_CANT_CREATE_CREDENTIAL_WITH_EMPTY_PASSWORD);
        }

        private readonly string password;

        private Credential(string password)
        {
            this.password = password;
        }

        public bool WithPassword(string password)
        {
            return this.password.Equals(password);
        }
    }
}