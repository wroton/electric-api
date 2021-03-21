namespace Service.Server.Entities
{
    /// <summary>
    /// A user registered in with application.
    /// </summary>
    public sealed class UserEntity
    {
        /// <summary>
        /// Unique id of the user.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Email address of the user.
        /// </summary>
        public string EmailAddress { get; private set; }

        /// <summary>
        /// Hashed password of the user.
        /// </summary>
        public string Password { get; private set; }
    }
}
