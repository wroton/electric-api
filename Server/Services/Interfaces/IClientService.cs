using System.Collections.Generic;
using System.Threading.Tasks;

using Service.Server.Models;

namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Handles client related requests.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Gets a list of clients to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the clients to which the caller has access.</returns>
        Task<IEnumerable<int>> List();

        /// <summary>
        /// Resolves a list of clients.
        /// </summary>
        /// <param name="ids">Ids of the clients to resolve.</param>
        /// <returns>Resolved clients.</returns>
        Task<IEnumerable<Client>> Resolve(IEnumerable<int> ids);

        /// <summary>
        /// Gets a client.
        /// </summary>
        /// <param name="id">Id of the client to get.</param>
        /// <returns>Client with the given id.</returns>
        Task<Client> Get(int id);

        /// <summary>
        /// Creates a client.
        /// </summary>
        /// <param name="client">Client to create.</param>
        /// <returns>Created client.</returns>
        Task<Client> Create(Client client);

        /// <summary>
        /// Updates a client.
        /// </summary>
        /// <param name="client">Client to update.</param>
        /// <returns>Updated client.</returns>
        Task<Client> Update(Client client);

        /// <summary>
        /// Deletes a client.
        /// </summary>
        /// <param name="id">Id of the client to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        Task Delete(int id);
    }
}
