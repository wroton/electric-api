using System.Collections.Generic;
using System.Threading.Tasks;

using Service.Server.Models;

namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Handles technician related requests.
    /// </summary>
    public interface ITechnicianService
    {
        /// <summary>
        /// Searches a list of technicians to which the caller has access.
        /// </summary>
        /// <param name="userId">Id of the user performing the search.</param>
        /// <param name="searchCriteria">Criteria by which the search should be performed.</param>
        /// <returns>List of ids of the technicians.</returns>
        Task<IEnumerable<int>> Search(int userId, TechnicianSearch searchCriteria);

        /// <summary>
        /// Resolves a list of technicians.
        /// </summary>
        /// <param name="userId">Id of the user requesting the technicians.</param>
        /// <param name="ids">Ids of the technicians to resolve.</param>
        /// <returns>Resolved technicians.</returns>
        Task<IEnumerable<Technician>> Resolve(int userId, IEnumerable<int> ids);

        /// <summary>
        /// Gets a technician.
        /// </summary>
        /// <param name="id">Id of the technician to get.</param>
        /// <returns>Technician with the given id.</returns>
        Task<Technician> Get(int id);

        /// <summary>
        /// Creates a technician.
        /// </summary>
        /// <param name="technician">Technician to create.</param>
        /// <returns>Created technician.</returns>
        Task<Technician> Create(Technician technician);

        /// <summary>
        /// Updates a technician.
        /// </summary>
        /// <param name="technician">Technician to update.</param>
        /// <returns>Updated technician.</returns>
        Task<Technician> Update(Technician technician);

        /// <summary>
        /// Deletes a technician.
        /// </summary>
        /// <param name="id">Id of the technician to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        Task Delete(int id);
    }
}
