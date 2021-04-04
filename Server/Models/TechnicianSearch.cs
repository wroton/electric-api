namespace Service.Server.Models
{
    /// <summary>
    /// Search request for technicians.
    /// </summary>
    public sealed class TechnicianSearch
    {
        /// <summary>
        /// Name of the technician.
        /// </summary>
        public string Name { get; set; }
    }
}
