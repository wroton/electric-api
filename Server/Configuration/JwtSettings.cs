using System.ComponentModel.DataAnnotations;

namespace Service.Server.Configuration
{
    /// <summary>
    /// Jwt settings used for signing and validating tokens..
    /// </summary>
    public sealed class JwtSettings
    {
        /// <summary>
        /// Key used to sign JWT tokens.
        /// </summary>
        [Required]
        [MinLength(16)]
        public byte[] Key { get; set; }

        /// <summary>
        /// Issuer to apply to tokens.
        /// </summary>
        [Required]
        public string Issuer { get; set; }
    }
}
