namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Processes hashing related tasks.
    /// </summary>
    public interface IHashService
    {
        /// <summary>
        /// Generates a random, 20 character salt.
        /// </summary>
        /// <returns>Randomly generated, 20 character salt.</returns>
        string GenerateSalt();

        /// <summary>
        /// Hashes a value.
        /// </summary>
        /// <param name="value">Vakue to hash.</param>
        /// <param name="salt">Salt to use.</param>
        /// <returns>Hashed value.</returns>
        string Hash(string value, string salt);

        /// <summary>
        /// Checks to see if a hashed and unhashed value are the same.
        /// </summary>
        /// <param name="hashedValue">Hashed value to use for comparison.</param>
        /// <param name="unhashedValue">Unhashed value to compare.</param>
        /// <returns>Are the values the same.</returns>
        bool Compare(string hashedValue, string unhashedValue);
    }
}
