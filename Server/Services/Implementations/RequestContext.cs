using Dapper;
using Microsoft.AspNetCore.Http;
using Service.Server.Entities;
using Service.Server.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Server.Services.Implementations
{
    /// <summary>
    /// Contains context information for the request.
    /// </summary>
    public sealed class RequestContext : IRequestContext
    {
        private readonly IDbConnectionFactory _connectionFactory;

        private UserEntity _user;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestContext" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">Http context accessor to use.</param>
        /// <param name="connectionFactory">Connection factory to use.</param>
        public RequestContext(IHttpContextAccessor httpContextAccessor, IDbConnectionFactory connectionFactory)
        {
            if (httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            // Do nothing if there is no user.
            if (httpContextAccessor.HttpContext?.User == null)
            {
                return;
            }

            // Do nothing if the id can't be parsed.
            if (!int.TryParse(httpContextAccessor.HttpContext.User.Identity.Name, out int userId))
            {
                return;
            }

            // Set the id.
            UserId = userId;

            // Set the user id and store the connection factory. Don't create one unless it is needed.
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /// <summary>
        /// Id of the user that submitted the request.
        /// Null if the calling user provided no authentication details.
        /// </summary>
        public int? UserId { get; private set; }

        /// <summary>
        /// Sets the user id.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        public void SetUserId(int userId)
        {
            UserId = userId;
        }

        /// <summary>
        /// Gets the user that submitted the request.
        /// Null if the calling user provided no authentication details.
        /// </summary>
        /// <returns>User that submitted the request.</returns>
        public async Task<UserEntity> User()
        {
            // If the user has already been resolved, return it.
            if (_user != null)
            {
                return _user;
            }

            // If there is no user, do nothing.
            if (!UserId.HasValue)
            {
                return null;
            }

            // Get the user.
            using var connection = _connectionFactory.Build();
            const string sql = "SELECT * FROM [User].vUsers WHERE Id = @Id;";
            var users = await connection.QueryAsync<UserEntity>(sql, new { Id = UserId });
            _user = users.FirstOrDefault();
            return _user;
        }
    }
}