using System.ComponentModel.DataAnnotations;

namespace Service.Server.Models
{
    /// <summary>
    /// A technician and its user record.
    /// </summary>
    public sealed class TechnicianUser
    {
        /// <summary>
        /// Information about the technician.
        /// </summary>
        [Required]
        public Technician Technician { get; set; }

        /// <summary>
        /// User record associated with the technician.
        /// </summary>
        [Required]
        public User User { get; set; }
    }
}
