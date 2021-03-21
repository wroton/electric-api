using System.Collections.Generic;
using System.Threading.Tasks;

using Service.Server.Models;

namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Handles business administrator related requests.
    /// </summary>
    public interface IBusinessAdministratorService
    {
        /// <summary>
        /// Gets a list of business administrators to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the business administrators to which the caller has access.</returns>
        public Task<IEnumerable<int>> List();

        /// <summary>
        /// Resolves a list of business administrators.
        /// </summary>
        /// <param name="ids">Ids of the business administrators to resolve.</param>
        /// <returns>Resolved business administrators.</returns>
        public Task<IEnumerable<BusinessAdministrator>> Resolve(IEnumerable<int> ids);

        /// <summary>
        /// Gets a business administrator.
        /// </summary>
        /// <param name="id">Id of the business administrator to get.</param>
        /// <returns>Business administrator with the given id.</returns>
        public Task<BusinessAdministrator> Get(int id);

        /// <summary>
        /// Creates a business administrator.
        /// </summary>
        /// <param name="administrator">Business administrator to create.</param>
        /// <returns>Created business administrator.</returns>
        public Task<BusinessAdministrator> Create(BusinessAdministrator administrator);

        /// <summary>
        /// Updates a business administrator.
        /// </summary>
        /// <param name="administrator">Business administrator to update.</param>
        /// <returns>Updated business administrator.</returns>
        public Task<BusinessAdministrator> Update(BusinessAdministrator administrator);

        /// <summary>
        /// Deletes a business administrator.
        /// </summary>
        /// <param name="id">Id of the business administrator to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        public Task Delete(int id);
    }
}
