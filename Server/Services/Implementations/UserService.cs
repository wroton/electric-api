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
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IHashService _hashService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="hashService">Hash service to use.</param>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public UserService(IHashService hashService, IDbConnectionFactory connectionFactory)
        {
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
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

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "User.Users_Resolve";
            var dbUsers = await connection.QueryAsync<UserEntity>(storedProcedure, new { Ids = splitIds }, commandType: CommandType.StoredProcedure);
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
            using var connection = _connectionFactory.Build();
            const string sql = "SELECT * FROM [User].vUsers WHERE Id = @id;";
            var dbUsers = await connection.QueryAsync<UserEntity>(sql, new { id });
            var createdUser = MapFromDB(dbUsers.SingleOrDefault());
            return createdUser;
        }

        /// <summary>
        /// Gets a user with the given email address.
        /// </summary>
        /// <param name="emailAddress">Email address of the user to get.</param>
        /// <returns>User with the corresponding email address. Null if the user was not found.</returns>
        public async Task<User> Get(string emailAddress)
        { 
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new WhiteSpaceException(nameof(emailAddress));
            }

            using var connection = _connectionFactory.Build();
            const string sql = "SELECT * FROM [User].vUsers WHERE EmailAddress = @emailAddress;";
            var dbUsers = await connection.QueryAsync<UserEntity>(sql, new { emailAddress });
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
            var hashedPassword = _hashService.Hash(user.NewPassword, salt);

            // Create a user.
            using var connection = _connectionFactory.Build();
            const string storedProcedure = "[User].User_Create";
            var dbUsers = await connection.QueryAsync<UserEntity>(storedProcedure, new { user.EmailAddress, Password = hashedPassword });
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
            var hashedPassword = _hashService.Hash(user.NewPassword, salt);

            // Create a user.
            using var connection = _connectionFactory.Build();
            const string storedProcedure = "[User].User_Update";
            var dbUsers = await connection.QueryAsync<UserEntity>(storedProcedure, new { user.Id, user.EmailAddress, Password = hashedPassword });
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
            using var connection = _connectionFactory.Build();
            const string storedProcedure = "[User].User_Delete";
            await connection.ExecuteAsync(storedProcedure, new { Id = id });
        }

        /// <summary>
        /// Maps a user from the database to its associated DTO.
        /// </summary>
        /// <param name="user">User to map.</param>
        /// <returns>Mapped user.</returns>
        public User MapFromDB(UserEntity user) => user == null ? null : new User
        {
            Id = user.Id,
            EmailAddress = user.EmailAddress,
            Password = user.Password
        };
    }
}
