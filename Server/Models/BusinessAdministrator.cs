using System.ComponentModel.DataAnnotations;

namespace Service.Server.Models
{
    /// <summary>
    /// Person that administrates a business.
    /// </summary>
    public sealed class BusinessAdministrator
    {
        /// <summary>
        /// Identifier of the administrator.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Name of the administrator. 
        /// </summary>
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Id of the business that the administrator administrates.
        /// This is set automatically and cannot be overridden.
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// Name of the business that the administrator administrates.
        /// This is set automatically and cannot be overridden.
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// Identifier of the user associated with the administrator.
        /// </summary>
        public int? UserId { get; set; }
    }
}
