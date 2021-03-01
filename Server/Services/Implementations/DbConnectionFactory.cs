using Microsoft.Extensions.Options;
using System;
using System.Data;

using Service.Server.Configuration;
using Service.Server.Services.Interfaces;
using System.Data.SqlClient;

namespace Service.Server.Services.Implementations
{
    /// <summary>
    /// Builds database connections.
    /// </summary>
    public sealed class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly ServiceSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnectionFactory" /> class.
        /// </summary>
        /// <param name="options">Options to use.</param>
        public DbConnectionFactory(IOptions<ServiceSettings> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _settings = options.Value;
        }

        /// <summary>
        /// Builds a database connection that is connected.
        /// </summary>
        /// <returns>Connected database connection.</returns>
        public IDbConnection Build()
        {
            var connection = new SqlConnection(_settings.ServiceConnectionString);
            connection.Open();
            return connection;
        }
    }
}
