using System.ComponentModel.DataAnnotations;

namespace Service.Server.DTOs
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
        /// First name of the technician.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the technician.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Id of the user record associated with this technician.
        /// If this is null, then the user does not have an associated login.
        /// </summary>
        public int? UserId { get; set; }
    }
}
