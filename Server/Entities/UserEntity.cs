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
        /// Unique username of the user.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Hashed password of the user.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Id of the business to which the user belongs.
        /// </summary>
        public int? BusinessId { get; private set; }

        /// <summary>
        /// Name of the business to which the user belongs.
        /// </summary>
        public string BusinessName { get; private set; }
    }
}
