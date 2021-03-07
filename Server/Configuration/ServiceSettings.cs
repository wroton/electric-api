using System.ComponentModel.DataAnnotations;

namespace Service.Server.Configuration
{
    /// <summary>
    /// Application settings.
    /// </summary>
    public sealed class ServiceSettings
    {
        /// <summary>
        /// Key used to sign JWT tokens.
        /// </summary>
        [Required]
        [MinLength(16)]
        public byte[] JwtKey { get; set; }

        /// <summary>
        /// Connection string used to connect to the Service database.
        /// </summary>
        [Required]
        public string ServiceConnectionString { get; set; }
    }
}
