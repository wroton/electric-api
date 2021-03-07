using System.Collections.Generic;
using System.Threading.Tasks;

using Service.Server.Models;

namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Handles business related requests.
    /// </summary>
    public interface IBusinessService
    {
        /// <summary>
        /// Gets a list of businesses to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the businesses to which the caller has access.</returns>
        Task<IEnumerable<int>> List();

        /// <summary>
        /// Resolves a list of businesses.
        /// </summary>
        /// <param name="ids">Ids of the businesses to resolve.</param>
        /// <returns>Resolved businesses.</returns>
        Task<IEnumerable<Business>> Resolve(IEnumerable<int> ids);

        /// <summary>
        /// Gets a business.
        /// </summary>
        /// <param name="id">Id of the business to get.</param>
        /// <returns>Business with the given id.</returns>
        Task<Business> Get(int id);

        /// <summary>
        /// Creates a business.
        /// </summary>
        /// <param name="business">Business to create.</param>
        /// <returns>Created business.</returns>
        Task<Business> Create(Business business);

        /// <summary>
        /// Updates a business.
        /// </summary>
        /// <param name="business">Business to update.</param>
        /// <returns>Updated business.</returns>
        Task<Business> Update(Business business);

        /// <summary>
        /// Deletes a business.
        /// </summary>
        /// <param name="id">Id of the business to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        Task Delete(int id);
    }
}
