using System.ComponentModel.DataAnnotations;

namespace Service.Server.Models
{
    /// <summary>
    /// A technician's position.
    /// </summary>
    public sealed class TechnicianPosition
    {
        /// <summary>
        /// Id of the position.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Name of the position.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Id of the business that created this position.
        /// </summary>
        public int? BusinessId { get; set; }
    }
}
