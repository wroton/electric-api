using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Server.DTOs
{
    /// <summary>
    /// A job to be worked.
    /// </summary>
    public sealed class Job
    {
        /// <summary>
        /// Date and time that the job is scheduled to end.
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Id of the job.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Date and time that the job is scheduled to start.
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Title of the job.
        /// </summary>
        [Required]
        public string Title { get; set; }
    }
}
