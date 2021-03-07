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

        /// <summary>
        /// Estimated cost of the work for the job.
        /// </summary>
        public decimal Estimate { get; private set; }

        /// <summary>
        /// Description of the job.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Can technicians assign themselves to the job.
        /// </summary>
        public bool OpenAssignment { get; private set; }

        /// <summary>
        /// Id of the business that owns the job.
        /// </summary>
        public int BusinessId { get; private set; }

        /// <summary>
        /// Name of the business that owns the job.
        /// </summary>
        public string BusinessName { get; private set; }

        /// <summary>
        /// Id of the client that requested the job be created.
        /// </summary>
        public int ClientId { get; private set; }

        /// <summary>
        /// Name of the client that requested the job be created.
        /// </summary>
        public string ClientName { get; private set; }

        /// <summary>
        /// Id of the technician to whom the job is assigned.
        /// </summary>
        public int? TechnicianId { get; private set; }

        /// <summary>
        /// Name of the technician to whom the job is assigned.
        /// </summary>
        public string TechnicianName { get; private set; }
    }
}
