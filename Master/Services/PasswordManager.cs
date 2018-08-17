using System;
using System.Security.Cryptography;

using Master.Interfaces.Services;

namespace Master.Services
{
    public class PasswordManager : IPasswordManager
    {
        const int SaltSize = 16;
        const int HashSize = 20;
        const int HashIterations = 10000;
        private byte[] Salt = new byte[SaltSize];
        private byte[] Hash;

        public PasswordManager()
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

        public bool VerifyPassword(string truePassword, string providedPassword)
        {
            // Convert password to bytes
            byte[] hashBytes = Convert.FromBase64String(truePassword);

            // Take the salt out
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 20, salt, 0, SaltSize);

            // Hash the user inputted password
            var hashProvidedPassword = new Rfc2898DeriveBytes(providedPassword, salt, HashIterations);

            byte[] hash = hashProvidedPassword.GetBytes(20);

            for(int i = 0; i < HashSize; i++)
                if(hashBytes[i] != hash[i])
                    return false;
            return true;
        }
    }
}