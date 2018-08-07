using System;
using System.Security.Cryptography;

namespace Master.Utilities
{
    public class PasswordHasher
    {
        const int SaltSize = 16;
        const int HashSize = 20;
        const int HashIterations = 10000;
        readonly byte[] Salt = new byte[SaltSize];
        readonly byte[] Hash;

        public PasswordHasher(string password)
        {
            // Generate a new salt
            new RNGCryptoServiceProvider().GetBytes(Salt);

            // Hash Both the password and Salt for the HashIterations
            var HashedString = new Rfc2898DeriveBytes(password, Salt, HashIterations);

            // Get the Hashed
            Hash = HashedString.GetBytes(20);
        }

        public string GeneratePassword()
        {
            byte[] PasswordBytes = new Byte[36];

            Array.Copy(Hash, 0, PasswordBytes, 0, HashSize);
            Array.Copy(Salt, 0, PasswordBytes, 20, SaltSize);

            string HashedPasswordString = Convert.ToBase64String(PasswordBytes);

            return HashedPasswordString;
        }
    }
}