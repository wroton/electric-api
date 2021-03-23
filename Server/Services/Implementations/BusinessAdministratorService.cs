using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Service.Server.Entities;
using Service.Server.Models;
using Service.Server.Services.Interfaces;

namespace Service.Server.Services.Implementations
{
    /// <summary>
    /// Handles business administrator related requests.
    /// </summary>
    public sealed class BusinessAdministratorService : IBusinessAdministratorService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessAdministratorService" /> class.
        /// </summary>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public BusinessAdministratorService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /// <summary>
        /// Gets a list of business administrators to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the business administrators to which the caller has access.</returns>
        public async Task<IEnumerable<int>> List()
        {
            using var connection = _connectionFactory.Build();
            const string sql = "SELECT Id FROM Administrator.vAdministrators";
            var dbIds = await connection.QueryAsync<int>(sql);
            return dbIds;
        }

        /// <summary>
        /// Resolves a list of business administrators.
        /// </summary>
        /// <param name="ids">Ids of the business administrators to resolve.</param>
        /// <returns>Resolved business administrators.</returns>
        public async Task<IEnumerable<BusinessAdministrator>> Resolve(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentException("Cannot be null or have a length of zero.", nameof(ids));
            }

            var splitIds = string.Join(',', ids);

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Administrator.Administrators_Resolve";
            var dbAdministrators = await connection.QueryAsync<BusinessAdministratorEntity>(storedProcedure, new { ids = splitIds }, commandType: CommandType.StoredProcedure);
            var administrators = dbAdministrators.Select(MapFromDB);
            return administrators;
        }

        /// <summary>
        /// Gets a business administrator.
        /// </summary>
        /// <param name="id">Id of the business administrator to get.</param>
        /// <returns>Business administrator with the given id.</returns>
        public async Task<BusinessAdministrator> Get(int id)
        {
            using var connection = _connectionFactory.Build();
            const string sql = "SELECT * FROM Administrator.vAdministrators WHERE Id = @id";
            var dbAdministrators = await connection.QueryAsync<BusinessAdministratorEntity>(sql, new { id });
            var administrator = MapFromDB(dbAdministrators.SingleOrDefault());
            return administrator;
        }

        /// <summary>
        /// Creates a business administrator.
        /// </summary>
        /// <param name="administrator">Business administrator to create.</param>
        /// <returns>Created business administrator.</returns>
        public async Task<BusinessAdministrator> Create(BusinessAdministrator administrator)
        {
            if (administrator == null)
            {
                throw new ArgumentNullException(nameof(administrator));
            }

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Administrator.Administrator_Create";
            var dbAdministrators = await connection.QueryAsync<BusinessAdministratorEntity>(storedProcedure, new
            {
                administrator.Name,
                administrator.BusinessName,
                administrator.UserId
            }, commandType: CommandType.StoredProcedure);
            var createdAdministrator = MapFromDB(dbAdministrators.FirstOrDefault());
            return createdAdministrator;
        }

        /// <summary>
        /// Updates a business administrator.
        /// </summary>
        /// <param name="administrator">Business administrator to update.</param>
        /// <returns>Updated business administrator.</returns>
        public async Task<BusinessAdministrator> Update(BusinessAdministrator administrator)
        {
            if (administrator == null)
            {
                throw new ArgumentNullException(nameof(administrator));
            }

            if (!administrator.Id.HasValue)
            {
                throw new ArgumentException("Id must be provided.", nameof(administrator));
            }

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Administrator.Business_Update";
            var dbAdministrators = await connection.QueryAsync<BusinessAdministratorEntity>(storedProcedure, new
            {
                administrator.Id,
                administrator.Name,
                administrator.BusinessId,
                administrator.UserId
            }, commandType: CommandType.StoredProcedure);
            var updatedAdministrator = MapFromDB(dbAdministrators.FirstOrDefault());
            return updatedAdministrator;
        }

        /// <summary>
        /// Deletes a business administrator.
        /// </summary>
        /// <param name="id">Id of the business administrator to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        public async Task Delete(int id)
        {
            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Administrator.Administrator_Delete";
            await connection.ExecuteAsync(storedProcedure, new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Maps a business administrator from the database to its associated DTO.
        /// </summary>
        /// <param name="administrator">Administrator to map.</param>
        /// <returns>Mapped administrator.</returns>
        private BusinessAdministrator MapFromDB(BusinessAdministratorEntity administrator) => administrator == null ? null : new BusinessAdministrator
        {
            Id = administrator.Id,
            Name = administrator.Name,
            BusinessId = administrator.BusinessId,
            BusinessName = administrator.BusinessName,
            UserId = administrator.UserId
        };
    }
}
