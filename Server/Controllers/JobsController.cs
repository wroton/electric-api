using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IJobService _jobService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobsController" /> class.
        /// </summary>
        /// <param name="jobService">Job service to use.</param>
        public JobsController(IJobService jobService)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
        }

        /// <summary>
        /// Gets a list of jobs to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the jobs to which the caller has access.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<int>), 200)]
        public async Task<IActionResult> List()
        {
            var ids = await _jobService.List();
            return Ok(ids);
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

            var jobs = await _jobService.Resolve(ids);
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
            var client = await _jobService.Get(id);
            if (client == null)
            {
                return NotFound("Job could not be found.");
            }

            return Ok(client);
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

            var createdJob = await _jobService.Create(job);
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

            var updatedJob = await _jobService.Update(job);
            if (updatedJob == null)
            {
                return NotFound("Job could not be found.");
            }

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
            var job = await _jobService.Get(id);
            if (job == null)
            {
                return NotFound("Job could not be found.");
            }

            await _jobService.Delete(id);

            return NoContent();
        }
    }
}
