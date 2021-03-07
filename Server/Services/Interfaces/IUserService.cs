using System.Collections.Generic;
using System.Threading.Tasks;

using Service.Server.Models;

namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Performs user related tasks.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Resolves a list of users.
        /// </summary>
        /// <param name="ids">Ids of the users to resolve.</param>
        /// <returns>Resolved users.</returns>
        Task<IEnumerable<User>> Resolve(IEnumerable<int> ids);

        /// <summary>
        /// Gets a user with the given id.
        /// </summary>
        /// <param name="id">Id of the user to get.</param>
        /// <returns>User with the corresponding id. Null if the user was not found.</returns>
        Task<User> Get(int id);

        /// <summary>
        /// Gets a user with the given username.
        /// </summary>
        /// <param name="username">Username of the user to get.</param>
        /// <returns>User with the corresponding username. Null if the user was not found.</returns>
        Task<User> Get(string username);

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="user">User to create.</param>
        /// <returns>Created user.</returns>
        Task<User> Create(User user);

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user">User to update.</param>
        /// <returns>Updated user.</returns>
        Task<User> Update(User user);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">Id of the user to delete.</param>
        /// <returns>Deleted user.</returns>
        Task Delete(int id);
    }
}
