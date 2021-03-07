using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

using Service.Server.Entities;
using Service.Server.Exceptions;
using Service.Server.Models;
using Service.Server.Services.Interfaces;

namespace Service.Server.Services.Implementations
{
    /// <summary>
    /// Performs user related tasks.
    /// </summary>
    public sealed class UserService : IUserService
    {
        private readonly IDbConnection _connection;
        private readonly IHashService _hashService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="hashService">Hash service to use.</param>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public UserService(IHashService hashService, IDbConnectionFactory connectionFactory)
        {
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));

            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _connection = connectionFactory.Build();
        }

        /// <summary>
        /// Resolves a list of users.
        /// </summary>
        /// <param name="ids">Ids of the users to resolve.</param>
        /// <returns>Resolved users.</returns>
        public async Task<IEnumerable<User>> Resolve(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentException("Cannot be null or have a length of zero.", nameof(ids));
            }

            var splitIds = string.Join(',', ids);

            const string storedProcedure = "User.Users_Resolve";
            var dbUsers = await _connection.QueryAsync<UserEntity>(storedProcedure, new { ids = splitIds }, commandType: CommandType.StoredProcedure);
            var users = dbUsers.Select(MapFromDB);
            return users;
        }

        /// <summary>
        /// Gets a user with the given id.
        /// </summary>
        /// <param name="id">Id of the user to get.</param>
        /// <returns>User with the corresponding id. Null if the user was not found.</returns>
        public async Task<User> Get(int id)
        {
            const string sql = "SELECT * FROM [User].vUsers WHERE Id = @id;";
            var dbUsers = await _connection.QueryAsync<UserEntity>(sql, new { id });
            var createdUser = MapFromDB(dbUsers.SingleOrDefault());
            return createdUser;
        }

        /// <summary>
        /// Gets a user with the given username.
        /// </summary>
        /// <param name="username">Username of the user to get.</param>
        /// <returns>User with the corresponding username. Null if the user was not found.</returns>
        public async Task<User> Get(string username)
        { 
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new WhiteSpaceException(nameof(username));
            }

            const string sql = "SELECT * FROM [User].vUsers WHERE Username = @username;";
            var dbUsers = await _connection.QueryAsync<UserEntity>(sql, new { username });
            var createdUser = MapFromDB(dbUsers.SingleOrDefault());
            return createdUser;
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="user">User to create.</param>
        /// <returns>Created user.</returns>
        public async Task<User> Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Hash the password using a random salt.
            var salt = _hashService.GenerateSalt();
            var hashedPassword = _hashService.Hash(user.Password, salt);

            // Create a user.
            const string storedProcedure = "[User].User_Create";
            var dbUsers = await _connection.QueryAsync<UserEntity>(storedProcedure, new { user.Username, Password = hashedPassword, user.BusinessId });
            var createdUser = MapFromDB(dbUsers.FirstOrDefault());
            return createdUser;
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user">User to update.</param>
        /// <returns>Updated user.</returns>
        public async Task<User> Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Hash the password using a random salt.
            var salt = _hashService.GenerateSalt();
            var hashedPassword = _hashService.Hash(user.Password, salt);

            // Create a user.
            const string storedProcedure = "[User].User_Update";
            var dbUsers = await _connection.QueryAsync<UserEntity>(storedProcedure, new { user.Id, user.Username, Password = hashedPassword });
            var updatedUser = MapFromDB(dbUsers.FirstOrDefault());
            return updatedUser;
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">Id of the user to delete.</param>
        /// <returns>Deleted user.</returns>
        public async Task Delete(int id)
        {
            // Delete the user.
            const string storedProcedure = "[User].User_Delete";
            await _connection.ExecuteAsync(storedProcedure, new { Id = id });
        }

        /// <summary>
        /// Maps a user from the database to its associated DTO.
        /// </summary>
        /// <param name="user">User to map.</param>
        /// <returns>Mapped user.</returns>
        private User MapFromDB(UserEntity user) => user == null ? null : new User
        {
            Id = user.Id,
            Username = user.Username,
            Password = user.Password,
            BusinessId = user.BusinessId,
            BusinessName = user.BusinessName
        };
    }
}
