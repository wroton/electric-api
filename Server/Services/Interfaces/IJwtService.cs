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
        string Token(int userId);
    }
}
