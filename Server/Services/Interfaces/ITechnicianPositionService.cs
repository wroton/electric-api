using System.Collections.Generic;
using System.Threading.Tasks;

using Service.Server.Models;

namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Handles technician position related requests.
    /// </summary>
    public interface ITechnicianPositionService
    {
        /// <summary>
        /// Gets a list of technician positions to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the technician positions to which the caller has access.</returns>
        Task<IEnumerable<int>> List();

        /// <summary>
        /// Resolves a list of technician positions.
        /// </summary>
        /// <param name="ids">Ids of the technician positions to resolve.</param>
        /// <returns>Resolved technician positions.</returns>
        Task<IEnumerable<TechnicianPosition>> Resolve(IEnumerable<int> ids);

        /// <summary>
        /// Gets a technician position.
        /// </summary>
        /// <param name="id">Id of the technician position to get.</param>
        /// <returns>Technician position with the given id.</returns>
        Task<TechnicianPosition> Get(int id);

        /// <summary>
        /// Creates a technician position.
        /// </summary>
        /// <param name="technicianPosition">Technician position to create.</param>
        /// <returns>Created technician position.</returns>
        Task<TechnicianPosition> Create(TechnicianPosition technicianPosition);

        /// <summary>
        /// Updates a technician position.
        /// </summary>
        /// <param name="technicianPosition">Technician position to update.</param>
        /// <returns>Updated technician position.</returns>
        Task<TechnicianPosition> Update(TechnicianPosition technicianPosition);

        /// <summary>
        /// Deletes a technician position.
        /// </summary>
        /// <param name="id">Id of the technician position to delete.</param>
        /// <returns>Task processing the request.</returns>
        Task Delete(int id);
    }
}
