namespace Service.Server.Entities
{
    /// <summary>
    /// A technician who works jobs in the field.
    /// </summary>
    public sealed class TechnicianEntity
    {
        /// <summary>
        /// Id of the technician.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Name of the technician.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Id of the user record associated with this technician.
        /// If this is null, then the user does not have an associated login.
        /// </summary>
        public int? UserId { get; private set; }
    }
}
