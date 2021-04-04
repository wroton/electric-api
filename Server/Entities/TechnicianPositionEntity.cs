namespace Service.Server.Entities
{
    /// <summary>
    /// A technician's position.
    /// </summary>
    public sealed class TechnicianPositionEntity
    {
        /// <summary>
        /// Id of the position.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Name of the position.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Id of the business that owns the technician position.
        /// </summary>
        public int BusinessId { get; private set; }
    }
}
