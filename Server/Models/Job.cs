using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Server.Models
{
    /// <summary>
    /// A job to be worked.
    /// </summary>
    public sealed class Job
    {
        /// <summary>
        /// Id of the job.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Title of the job.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// Date and time that the job is scheduled to start.
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Date and time that the job is scheduled to end.
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Description of the job.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Can technicians assign themselves to the job.
        /// </summary>
        [Required]
        public bool OpenAssignment { get; set; }

        /// <summary>
        /// Id of the business that owns the job.
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// Name of the business that owns the job.
        /// </summary>
        [MaxLength(200)]
        public string BusinessName { get; set; }

        /// <summary>
        /// Id of the client that requested the job be created.
        /// </summary>
        [Required]
        public int ClientId { get; set; }

        /// <summary>
        /// Name of the client that requested the job be created.
        /// </summary>
        [MaxLength(200)]
        public string ClientName { get; set; }

        /// <summary>
        /// Id of the technician to whom the job is assigned.
        /// </summary>
        public int? TechnicianId { get; set; }

        /// <summary>
        /// Name of the technician to whom the job is assigned.
        /// </summary>
        [MaxLength(100)]
        public string TechnicianName { get; set; }
    }
}
