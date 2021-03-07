using System.Collections.Generic;
using System.Threading.Tasks;

using Service.Server.Models;

namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Handles job related requests.
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// Gets a list of jobs to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the jobs to which the caller has access.</returns>
        Task<IEnumerable<int>> List();

        /// <summary>
        /// Resolves a list of jobs.
        /// </summary>
        /// <param name="ids">Ids of the jobs to resolve.</param>
        /// <returns>Resolved jobs.</returns>
        Task<IEnumerable<Job>> Resolve(IEnumerable<int> ids);

        /// <summary>
        /// Gets a job.
        /// </summary>
        /// <param name="id">Id of the job to get.</param>
        /// <returns>Job with the given id.</returns>
        Task<Job> Get(int id);

        /// <summary>
        /// Creates a job.
        /// </summary>
        /// <param name="job">Job to create.</param>
        /// <returns>Created job.</returns>
        Task<Job> Create(Job job);

        /// <summary>
        /// Updates a job.
        /// </summary>
        /// <param name="job">Job to update.</param>
        /// <returns>Updated job.</returns>
        Task<Job> Update(Job job);

        /// <summary>
        /// Deletes a job.
        /// </summary>
        /// <param name="id">Id of the job to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        Task Delete(int id);
    }
}
