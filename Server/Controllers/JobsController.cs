using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Service.Server.Entities;
using Service.Server.Models;
using Service.Server.Services.Interfaces;

namespace Service.Server.Controllers
{
    /// <summary>
    /// Handles job related requests.
    /// </summary>
    [Route("api/1/jobs")]
    public class JobsController : BaseController
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobsController" /> class.
        /// </summary>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public JobsController(IDbConnectionFactory connectionFactory)
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
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<int>), 200)]
        public async Task<IActionResult> List()
        {
            const string sql = "SELECT Id FROM Job.vJobs";
            var dbIds = await _connection.QueryAsync<int>(sql);
            return Ok(dbIds);
        }

        /// <summary>
        /// Resolves a list of jobs.
        /// </summary>
        /// <param name="ids">Ids of the jobs to resolve.</param>
        /// <returns>Resolved jobs.</returns>
        [HttpPost]
        [Route("resolve")]
        [ProducesResponseType(typeof(IEnumerable<Job>), 200)]
        public async Task<IActionResult> Resolve([FromBody] IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Ok(Array.Empty<Job>());
            }

            var splitIds = string.Join(',', ids);

            const string storedProcedure = "Job.Jobs_Resolve";
            var dbJobs = await _connection.QueryAsync<JobEntity>(storedProcedure, new { ids = splitIds }, commandType: CommandType.StoredProcedure);
            var jobs = dbJobs.Select(MapFromDB);
            return Ok(jobs);
        }

        /// <summary>
        /// Gets a job.
        /// </summary>
        /// <param name="id">Id of the job to get.</param>
        /// <returns>Job with the given id.</returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(Job), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var dbJob = await GetJobById(id);
            if (dbJob == null)
            {
                return NotFound("Job could not be found.");
            }

            var job = MapFromDB(dbJob);
            return Ok(job);
        }

        /// <summary>
        /// Creates a job.
        /// </summary>
        /// <param name="job">Job to create.</param>
        /// <returns>Created job.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Job), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody] Job job)
        {
            if (job == null)
            {
                return BadRequest("Job was not provided in the body or could not be interpreted as JSON.");
            }

            const string storedProcedure = "Job.Job_Create";
            var dbJobs = await _connection.QueryAsync<JobEntity>(storedProcedure, new { job.Title, job.StartTime, job.EndTime }, commandType: CommandType.StoredProcedure);
            var createdJob = MapFromDB(dbJobs.FirstOrDefault());
            return Ok(createdJob);
        }

        /// <summary>
        /// Updates a job.
        /// </summary>
        /// <param name="job">Job to update.</param>
        /// <returns>Updated job.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(Job), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Put([FromBody] Job job)
        {
            if (job == null)
            {
                return BadRequest("Job was not provided in the body or could not be interpreted as JSON.");
            }

            if (!job.Id.HasValue)
            {
                return BadRequest("Job id must be provided.");
            }

            var dbJob = await GetJobById(job.Id.Value);
            if (dbJob == null)
            {
                return NotFound("Job could not be found.");
            }

            const string storedProcedure = "Job.Job_Update";
            var dbJobs = await _connection.QueryAsync<JobEntity>(storedProcedure, new { job.Id, job.Title, job.StartTime, job.EndTime }, commandType: CommandType.StoredProcedure);
            var updatedJob = MapFromDB(dbJobs.FirstOrDefault());
            return Ok(updatedJob);
        }

        /// <summary>
        /// Deletes a job.
        /// </summary>
        /// <param name="id">Id of the job to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(Job), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var dbJob = await GetJobById(id);
            if (dbJob == null)
            {
                return NotFound("Job could not be found.");
            }

            const string storedProcedure = "Job.Job_Delete";
            await _connection.ExecuteAsync(storedProcedure, new { Id = id }, commandType: CommandType.StoredProcedure);
            return Ok();
        }

        /// <summary>
        /// Gets a job by its id.
        /// </summary>
        /// <param name="id">Id of the job to get.</param>
        /// <returns>Job with the given id.</returns>
        private async Task<JobEntity> GetJobById(int id)
        {
            const string sql = "SELECT * FROM Job.vJobs WHERE Id = @id";
            var dbJobs = await _connection.QueryAsync<JobEntity>(sql, new { id });
            return dbJobs.FirstOrDefault();
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
            EndTime = job.EndTime
        };
    }
}
