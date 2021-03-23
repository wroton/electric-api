namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Performs JWT related functions.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Creates a signed jwt token for a user.
        /// </summary>
        /// <param name="userId">Id of the user for whom the token is being created.</param>
        /// <returns>Signed token.</returns>
        string Create(int userId);


        /// <summary>
        /// Reads the id from a signed jwt token.
        /// </summary>
        /// <param name="token">Signed token from which the id should be read.</param>
        /// <returns>Id from the jwt token. Null if the token couldn't be read.</returns>
        int? Read(string token);
    }
}
