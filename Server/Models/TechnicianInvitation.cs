using System.ComponentModel.DataAnnotations;

namespace Service.Server.Models
{
    /// <summary>
    /// An invitation to become a technician.
    /// </summary>
    public sealed class TechnicianInvitation
    {
        /// <summary>
        /// Id of the technician that the invtee will assume.
        /// </summary>
        public int TechnicianId { get; set; }

        /// <summary>
        /// Email address of the user that is being invited.
        /// </summary>
        [Required]
        [StringLength(320, MinimumLength = 8)]
        public string EmailAddress { get; set; }
    }
}
