using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Service.Server.Models
{
    /// <summary>
    /// A user registered in the application.
    /// </summary>
    public sealed class User
    {
        /// <summary>
        /// Unique id of the user.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Unique username of the user.
        /// </summary>
        [Required]
        [StringLength(320, MinimumLength = 5)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Password of the user.
        /// </summary>
        [MinLength(8)]
        [JsonIgnore]
        public string Password { get; set; }

        /// <summary>
        /// New password to apply to the user.
        /// </summary>
        [MinLength(8)]
        public string NewPassword { get; set; }
    }
}
