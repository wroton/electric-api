using System;

namespace Service.Server.Entities
{
    /// <summary>
    /// A job to be worked.
    /// </summary>
    public sealed class JobEntity
    {
        /// <summary>
        /// Id of the job.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Title of the job.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Date and time that the job is scheduled to start.
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// Date and time that the job is scheduled to end.
        /// </summary>
        public DateTime EndTime { get; private set; }
    }
}
