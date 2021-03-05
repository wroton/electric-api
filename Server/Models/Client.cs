using System.ComponentModel.DataAnnotations;

namespace Service.Server.Models
{
    /// <summary>
    /// A client served by a business.
    /// </summary>
    public sealed class Client
    {
        /// <summary>
        /// Unique id of the client.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the client.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Address line 1 of the client's location.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Address line 1 of the client's location.
        /// </summary>
        [MaxLength(150)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// City of the client's location.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        /// <summary>
        /// State of the client's location.
        /// </summary>
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; }

        /// <summary>
        /// Zip code of the client's location.
        /// </summary>
        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string ZipCode { get; set; }
    }
}
