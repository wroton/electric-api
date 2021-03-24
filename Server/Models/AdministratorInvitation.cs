using System.ComponentModel.DataAnnotations;

namespace Service.Server.Models
{
    /// <summary>
    /// An invitation to become an administrator.
    /// </summary>
    public sealed class AdministratorInvitation
    {
        /// <summary>
        /// Id of the administrator that the invitee will assume.
        /// </summary>
        public int AdministratorId { get; set; }

        /// <summary>
        /// Email address of the user that is being invited.
        /// </summary>
        [Required]
        [StringLength(320, MinimumLength = 8)]
        public string EmailAddress { get; set; }
    }
}
