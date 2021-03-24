using System.ComponentModel.DataAnnotations;

namespace Service.Server.Models
{
    /// <summary>
    /// An accepted signup for an administrator.
    /// </summary>
    public sealed class AdministratorSignup
    {
        /// <summary>
        /// Email address of the user accepting the administrator invitation.
        /// If the email address doesn't exist, a user with that email address will be created.
        /// </summary>
        [Required]
        [StringLength(320, MinimumLength = 5)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Password of the user accepting the administrator invitation.
        /// If the user doesn't already exist, a user with this password will be created.
        /// </summary>
        [MinLength(8)]
        public string Password { get; set; }

        /// <summary>
        /// Invitation token the user received for the administrator role.
        /// </summary>
        [Required]
        public string InvitationToken { get; set; }
    }
}
