namespace Service.Server.Entities
{
    /// <summary>
    /// Person that administrates a business.
    /// </summary>
    public sealed class AdministratorEntity
    {
        /// <summary>
        /// Identifier of the administrator.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Name of the administrator. 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Id of the business that the administrator administrates.
        /// </summary>
        public int BusinessId { get; private set; }

        /// <summary>
        /// Name of the business that the administrator administrates.
        /// </summary>
        public string BusinessName { get; private set; }

        /// <summary>
        /// Identifier of the user associated with the administrator.
        /// </summary>
        public int? UserId { get; private set; }
    }
}
