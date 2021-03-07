using System.ComponentModel.DataAnnotations;

namespace Service.Server.Models
{
    /// <summary>
    /// Details needed to authentication a user.
    /// </summary>
    public sealed class AuthenticationDetails
    {
        /// <summary>
        /// Username of the user attempting to authenticate.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Username { get; set; }

        /// <summary>
        /// Password of the user attempting to authenticate.
        /// </summary>
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
