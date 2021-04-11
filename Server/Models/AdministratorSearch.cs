namespace Service.Server.Models
{
    /// <summary>
    /// Search request for administrators.
    /// </summary>
    public sealed class AdministratorSearch
    {
        /// <summary>
        /// Name of the administrator.
        /// </summary>
        public string Name { get; set; }
    }
}
