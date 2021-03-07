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
        [StringLength(100, MinimumLength = 8)]
        public string Username { get; set; }

        /// <summary>
        /// Password of the user.
        /// </summary>
        [MinLength(8)]
        [JsonIgnore]
        public string Password { get; set; }

        /// <summary>
        /// Id of the business to which the user belongs.
        /// </summary>
        public int? BusinessId { get; set; }

        /// <summary>
        /// Name of the business to which the user belongs.
        /// </summary>
        public string BusinessName { get; set; }
    }
}
