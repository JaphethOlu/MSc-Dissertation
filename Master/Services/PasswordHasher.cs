using System;
using System.Security.Cryptography;

using Master.Interfaces.Services;

namespace Master.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        const int SaltSize = 16;
        const int HashSize = 20;
        const int HashIterations = 10000;
        private byte[] Salt = new byte[SaltSize];
        private byte[] Hash;

        public PasswordHasher()
        {
            // Generate a new salt
            new RNGCryptoServiceProvider().GetBytes(Salt);            
        }

        public string GeneratePassword(string password)
		{
			// Hash Both the password and Salt for the HashIterations
			var HashedString = new Rfc2898DeriveBytes(password, Salt, HashIterations);

			// Get the Hashed
			Hash = HashedString.GetBytes(20);


			byte[] PasswordBytes = new Byte[36];

            Array.Copy(Hash, 0, PasswordBytes, 0, HashSize);
            Array.Copy(Salt, 0, PasswordBytes, 20, SaltSize);

            string HashedPasswordString = Convert.ToBase64String(PasswordBytes);

            return HashedPasswordString;
        }
    }
}