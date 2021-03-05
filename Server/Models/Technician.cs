using System.ComponentModel.DataAnnotations;

namespace Service.Server.Models
{
    /// <summary>
    /// A technician who works jobs in the field.
    /// </summary>
    public sealed class Technician
    {
        /// <summary>
        /// Id of the technician.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Name of the technician.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Id of the user record associated with this technician.
        /// If this is null, then the user does not have an associated login.
        /// </summary>
        public int? UserId { get; set; }
    }
}
