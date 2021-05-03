using System.ComponentModel.DataAnnotations;

namespace Service.Server.Models
{
    /// <summary>
    /// A registered business.
    /// </summary>
    public sealed class Business
    {
        /// <summary>
        /// Unique id of the business.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Name of the business.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Address line 1 of the business' location.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Address line 1 of the business' location.
        /// </summary>
        [MinLength(1)]
        [MaxLength(150)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// City of the business' location.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        /// <summary>
        /// State of the business' location.
        /// </summary>
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; }

        /// <summary>
        /// Zip code of the business' location.
        /// </summary>
        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string ZipCode { get; set; }
    }
}
