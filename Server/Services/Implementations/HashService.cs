using System;
using System.Security.Cryptography;
using System.Text;

using Service.Server.Exceptions;
using Service.Server.Services.Interfaces;

namespace Service.Server.Services.Implementations
{
    /// <summary>
    /// Processes hashing related tasks.
    /// </summary>
    public sealed class HashService : IHashService
    {
        /// <summary>
        /// Generates a random, 20 character salt.
        /// </summary>
        /// <returns>Randomly generated, 20 character salt.</returns>
        public string GenerateSalt()
        {
            // Create a random generator.
            Random random = new Random();

            // Create 20 random characters.
            var stringBuilder = new StringBuilder();
            for (byte i = 0; i < 20; i++)
            {
                var randomChar = (char)random.Next(char.MinValue, char.MaxValue);
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Hashes a value.
        /// </summary>
        /// <param name="value">Vakue to hash.</param>
        /// <param name="salt">Salt to use.</param>
        /// <returns>Hashed value.</returns>
        public string Hash(string value, string salt)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new WhiteSpaceException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new WhiteSpaceException(nameof(salt));
            }

            // Combine the value and the salt then encode it..
            var saltedValue = value + salt;
            var encodedValue = Encoding.UTF8.GetBytes(saltedValue);

            // Hash using SHA-256.
            SHA256Managed hasher = new SHA256Managed();

            // Hash the salted value.
            var hashedPassword = hasher.ComputeHash(encodedValue);

            // Convert the hashed password to base64 then combine it with the salt.
            var base64Password = Convert.ToBase64String(hashedPassword) + salt;
            return base64Password;
        }

        /// <summary>
        /// Checks to see if a hashed and unhashed value are the same.
        /// </summary>
        /// <param name="hashedValue">Hashed value to use for comparison. This assumes the hash algroythm was SHA-256 and the salt is appended.</param>
        /// <param name="unhashedValue">Unhashed value to compare.</param>
        /// <returns>Are the values the same.</returns>
        public bool Compare(string hashedValue, string unhashedValue)
        {
            if (string.IsNullOrWhiteSpace(hashedValue))
            {
                throw new WhiteSpaceException(nameof(hashedValue));
            }

            if (hashedValue.Length < 45)
            {
                throw new ArgumentException("Must be greater than 32 characters in length.", nameof(hashedValue));
            }

            if (string.IsNullOrWhiteSpace(unhashedValue))
            {
                throw new WhiteSpaceException(nameof(unhashedValue));
            }

            // Split the salt from the hashed value. The salt is every character after the 32nd one.
            var salt = hashedValue[44..];

            // Has the unhashed value.
            var hashedComparison = Hash(unhashedValue, salt);

            // Compare the hashed value and the new hashed value.
            return hashedValue == hashedComparison;
        }
    }
}
