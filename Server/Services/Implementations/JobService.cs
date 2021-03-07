using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Service.Server.Entities;
using Service.Server.Models;
using Service.Server.Services.Interfaces;

namespace Service.Server.Services.Implementations
{
    /// <summary>
    /// Handles job related requests.
    /// </summary>
    public class JobService : IJobService
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobService" /> class.
        /// </summary>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public JobService(IDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _connection = connectionFactory.Build();
        }

        /// <summary>
        /// Gets a list of jobs to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the jobs to which the caller has access.</returns>
        public async Task<IEnumerable<int>> List()
        {
            const string sql = "SELECT Id FROM Job.vJobs";
            var dbIds = await _connection.QueryAsync<int>(sql);
            return dbIds;
        }

        /// <summary>
        /// Resolves a list of jobs.
        /// </summary>
        /// <param name="ids">Ids of the jobs to resolve.</param>
        /// <returns>Resolved jobs.</returns>
        public async Task<IEnumerable<Job>> Resolve(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentException("Cannot be null or have a length of zero.", nameof(ids));
            }

            var splitIds = string.Join(',', ids);

            const string storedProcedure = "Job.Jobs_Resolve";
            var dbJobs = await _connection.QueryAsync<JobEntity>(storedProcedure, new { ids = splitIds }, commandType: CommandType.StoredProcedure);
            var jobs = dbJobs.Select(MapFromDB);
            return jobs;
        }

        /// <summary>
        /// Gets a job.
        /// </summary>
        /// <param name="id">Id of the job to get.</param>
        /// <returns>Job with the given id.</returns>
        public async Task<Job> Get(int id)
        {
            const string sql = "SELECT * FROM Job.vJobs WHERE Id = @id";
            var dbJobs = await _connection.QueryAsync<JobEntity>(sql, new { id });
            var job = MapFromDB(dbJobs.SingleOrDefault());
            return job;
        }

        /// <summary>
        /// Creates a job.
        /// </summary>
        /// <param name="job">Job to create.</param>
        /// <returns>Created job.</returns>
        public async Task<Job> Create(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }

            const string storedProcedure = "Job.Job_Create";
            var dbJobs = await _connection.QueryAsync<JobEntity>(storedProcedure, new
            {
                job.Title,
                job.StartTime,
                job.EndTime,
                job.Estimate,
                job.OpenAssignment,
                job.Description,
                BusinessId = 0, // Assign a business id.
                job.ClientId,
                job.TechnicianId
            }, commandType: CommandType.StoredProcedure);
            var createdJob = MapFromDB(dbJobs.FirstOrDefault());
            return createdJob;
        }

        /// <summary>
        /// Updates a job.
        /// </summary>
        /// <param name="job">Job to update.</param>
        /// <returns>Updated job.</returns>
        public async Task<Job> Update(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }

            if (!job.Id.HasValue)
            {
                throw new ArgumentException("Id must be provided.", nameof(job));
            }

            const string storedProcedure = "Job.Job_Update";
            var dbJobs = await _connection.QueryAsync<JobEntity>(storedProcedure, new
            {
                job.Id,
                job.Title,
                job.StartTime,
                job.EndTime,
                job.Estimate,
                job.OpenAssignment,
                job.Description,
                BusinessId = 0, // Assign a business id.
                job.ClientId,
                job.TechnicianId
            }, commandType: CommandType.StoredProcedure);
            var updatedJob = MapFromDB(dbJobs.FirstOrDefault());
            return updatedJob;
        }

        /// <summary>
        /// Deletes a job.
        /// </summary>
        /// <param name="id">Id of the job to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        public async Task Delete(int id)
        {
            const string storedProcedure = "Job.Job_Delete";
            await _connection.ExecuteAsync(storedProcedure, new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Maps a job from the database to its associated DTO.
        /// </summary>
        /// <param name="job">Job to map.</param>
        /// <returns>Mapped job.</returns>
        private Job MapFromDB(JobEntity job) => job == null ? null : new Job
        {
            Id = job.Id,
            Title = job.Title,
            StartTime = job.StartTime,
            EndTime = job.EndTime,
            Estimate = job.Estimate,
            OpenAssignment = job.OpenAssignment,
            Description = job.Description,
            BusinessId = job.BusinessId,
            ClientId = job.ClientId,
            TechnicianId = job.TechnicianId
        };
    }
}
