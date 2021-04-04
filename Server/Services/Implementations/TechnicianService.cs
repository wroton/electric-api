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
    /// Handles technician related requests.
    /// </summary>
    public class TechnicianService : ITechnicianService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicianService" /> class.
        /// </summary>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public TechnicianService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /// <summary>
        /// Searches a list of technicians to which the caller has access.
        /// </summary>
        /// <param name="userId">Id of the user performing the search.</param>
        /// <param name="searchCriteria">Criteria by which the search should be performed.</param>
        /// <returns>List of ids of the technicians.</returns>
        public async Task<IEnumerable<int>> Search(int userId, TechnicianSearch searchCriteria)
        {
            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Technician.Technicians_Search";
            var dbIds = await connection.QueryAsync<int>(storedProcedure, new { searchCriteria.Name, userId }, commandType: CommandType.StoredProcedure );
            return dbIds;
        }

        /// <summary>
        /// Resolves a list of technicians.
        /// </summary>
        /// <param name="userId">Id of the user requesting the technicians.</param>
        /// <param name="ids">Ids of the technicians to resolve.</param>
        /// <returns>Resolved technicians.</returns>
        public async Task<IEnumerable<Technician>> Resolve(int userId, IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentException("Cannot be null or have a length of zero.", nameof(ids));
            }

            var splitIds = string.Join(',', ids);

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Technician.Technicians_Resolve";
            var dbTechnicians = await connection.QueryAsync<TechnicianEntity>(storedProcedure, new { userId, ids = splitIds }, commandType: CommandType.StoredProcedure);
            var technicians = dbTechnicians.Select(MapFromDB);
            return technicians;
        }

        /// <summary>
        /// Gets a technician.
        /// </summary>
        /// <param name="id">Id of the technician to get.</param>
        /// <returns>Technician with the given id.</returns>
        public async Task<Technician> Get(int id)
        {
            using var connection = _connectionFactory.Build();
            const string sql = "SELECT * FROM Technician.vTechnicians WHERE Id = @id";
            var dbTechnicians = await connection.QueryAsync<TechnicianEntity>(sql, new { id });
            var technician = MapFromDB(dbTechnicians.SingleOrDefault());
            return technician;
        }

        /// <summary>
        /// Creates a technician.
        /// </summary>
        /// <param name="technician">Technician to create.</param>
        /// <returns>Created technician.</returns>
        public async Task<Technician> Create(Technician technician)
        {
            if (technician == null)
            {
                throw new ArgumentNullException(nameof(technician));
            }

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Technician.Technician_Create";
            var dbTechnicians = await connection.QueryAsync<TechnicianEntity>(storedProcedure, new
            {
                technician.Name,
                technician.PositionId,
                technician.BusinessId,
                technician.UserId
            }, commandType: CommandType.StoredProcedure);
            var createdTechnician = MapFromDB(dbTechnicians.FirstOrDefault());
            return createdTechnician;
        }

        /// <summary>
        /// Updates a technician.
        /// </summary>
        /// <param name="technician">Technician to update.</param>
        /// <returns>Updated technician.</returns>
        public async Task<Technician> Update(Technician technician)
        {
            if (technician == null)
            {
                throw new ArgumentNullException(nameof(technician));
            }

            if (!technician.Id.HasValue)
            {
                throw new ArgumentException("Id must be provided.", nameof(technician));
            }

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Technician.Technician_Update";
            var dbTechnicians = await connection.QueryAsync<TechnicianEntity>(storedProcedure, new
            {
                technician.Id,
                technician.Name,
                technician.PositionId
            }, commandType: CommandType.StoredProcedure);
            var updatedTechnician = MapFromDB(dbTechnicians.FirstOrDefault());
            return updatedTechnician;
        }

        /// <summary>
        /// Deletes a technician.
        /// </summary>
        /// <param name="id">Id of the technician to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        public async Task Delete(int id)
        {
            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Technician.Technician_Delete";
            await connection.ExecuteAsync(storedProcedure, new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Maps a technician from the database to its associated DTO.
        /// </summary>
        /// <param name="technician">Technician to map.</param>
        /// <returns>Mapped technician.</returns>
        private Technician MapFromDB(TechnicianEntity technician) => technician == null ? null : new Technician
        {
            Id = technician.Id,
            Name = technician.Name,
            PositionId = technician.PositionId,
            PositionName = technician.PositionName,
            BusinessId = technician.BusinessId,
            BusinessName = technician.BusinessName,
            UserId = technician.UserId
        };
    }
}
