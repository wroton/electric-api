using System.Collections.Generic;
using System.Threading.Tasks;

using Service.Server.Models;

namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Handles business administrator related requests.
    /// </summary>
    public interface IAdministratorService
    {
        /// <summary>
        /// Gets a list of business administrators to which the caller has access.
        /// </summary>
        /// <param name="userId">Id of the user requesting the administrator list.</param>
        /// <returns>List of ids of the business administrators to which the caller has access.</returns>
        public Task<IEnumerable<int>> List(int userId);

        /// <summary>
        /// Searches a list of administrators to which the caller has access.
        /// </summary>
        /// <param name="userId">Id of the user performing the search.</param>
        /// <param name="searchCriteria">Criteria by which the search should be performed.</param>
        /// <returns>List of ids of the administrators.</returns>
        Task<IEnumerable<int>> Search(int userId, AdministratorSearch searchCriteria);

        /// <summary>
        /// Resolves a list of business administrators.
        /// </summary>
        /// <param name="userId">Id of the user resolving the administrators.</param>
        /// <param name="ids">Ids of the business administrators to resolve.</param>
        /// <returns>Resolved business administrators.</returns>
        public Task<IEnumerable<Administrator>> Resolve(int userId, IEnumerable<int> ids);

        /// <summary>
        /// Gets a business administrator.
        /// </summary>
        /// <param name="id">Id of the business administrator to get.</param>
        /// <returns>Business administrator with the given id.</returns>
        public Task<Administrator> Get(int id);

        /// <summary>
        /// Creates a business administrator.
        /// </summary>
        /// <param name="administrator">Business administrator to create.</param>
        /// <returns>Created business administrator.</returns>
        public Task<Administrator> Create(Administrator administrator);

        /// <summary>
        /// Updates a business administrator.
        /// </summary>
        /// <param name="administrator">Business administrator to update.</param>
        /// <returns>Updated business administrator.</returns>
        public Task<Administrator> Update(Administrator administrator);

        /// <summary>
        /// Deletes a business administrator.
        /// </summary>
        /// <param name="id">Id of the business administrator to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        public Task Delete(int id);
    }
}
