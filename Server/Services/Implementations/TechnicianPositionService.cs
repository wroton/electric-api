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
    /// Handles technician position related requests.
    /// </summary>
    public class TechnicianPositionService : ITechnicianPositionService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicianPositionService" /> class.
        /// </summary>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public TechnicianPositionService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /// <summary>
        /// Gets a list of technician positions to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the technician positions to which the caller has access.</returns>
        public async Task<IEnumerable<int>> List()
        {
            using var connection = _connectionFactory.Build();
            const string sql = "SELECT Id FROM Technician.vTechnicianPositions";
            var dbIds = await connection.QueryAsync<int>(sql);
            return dbIds;
        }

        /// <summary>
        /// Resolves a list of technician positions.
        /// </summary>
        /// <param name="ids">Ids of the technician positions to resolve.</param>
        /// <returns>Resolved technician positions.</returns>
        public async Task<IEnumerable<TechnicianPosition>> Resolve(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentException("Cannot be null or have a length of zero.", nameof(ids));
            }

            var splitIds = string.Join(',', ids);

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Technician.TechnicianPositions_Resolve";
            var dbTechnicianPositions = await connection.QueryAsync<TechnicianPositionEntity>(storedProcedure, new { ids = splitIds }, commandType: CommandType.StoredProcedure);
            var technicianPositions = dbTechnicianPositions.Select(MapFromDB);
            return technicianPositions;
        }

        /// <summary>
        /// Gets a technician position.
        /// </summary>
        /// <param name="id">Id of the technician position to get.</param>
        /// <returns>Technician position with the given id.</returns>
        public async Task<TechnicianPosition> Get(int id)
        {
            using var connection = _connectionFactory.Build();
            const string sql = "SELECT * FROM Technician.vTechnicianPositions WHERE Id = @id";
            var dbTechnicianPositions = await connection.QueryAsync<TechnicianPositionEntity>(sql, new { id });
            var technicianPosition = MapFromDB(dbTechnicianPositions.SingleOrDefault());
            return technicianPosition;
        }

        /// <summary>
        /// Creates a technician position.
        /// </summary>
        /// <param name="technicianPosition">Technician position to create.</param>
        /// <returns>Created technician position.</returns>
        public async Task<TechnicianPosition> Create(TechnicianPosition technicianPosition)
        {
            if (technicianPosition == null)
            {
                throw new ArgumentNullException(nameof(technicianPosition));
            }

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Technician.vTechnicianPosition_Create";
            var dbTechnicianPositions = await connection.QueryAsync<TechnicianPositionEntity>(storedProcedure, new
            {
                technicianPosition.Name,
                BusinessId = 0 // Assign the business id.
            }, commandType: CommandType.StoredProcedure);
            var createdTechnicianPosition = MapFromDB(dbTechnicianPositions.FirstOrDefault());
            return createdTechnicianPosition;
        }

        /// <summary>
        /// Updates a technician position.
        /// </summary>
        /// <param name="technicianPosition">Technician position to update.</param>
        /// <returns>Updated technician position.</returns>
        public async Task<TechnicianPosition> Update(TechnicianPosition technicianPosition)
        {
            if (technicianPosition == null)
            {
                throw new ArgumentNullException(nameof(technicianPosition));
            }

            if (!technicianPosition.Id.HasValue)
            {
                throw new ArgumentException("Id must be provided.", nameof(technicianPosition));
            }

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Technician.TechnicianPosition_Update";
            var dbTechnicianPositions = await connection.QueryAsync<TechnicianPositionEntity>(storedProcedure, new
            {
                technicianPosition.Id,
                technicianPosition.Name
            }, commandType: CommandType.StoredProcedure);
            var udpatedTechnicianPosition = MapFromDB(dbTechnicianPositions.FirstOrDefault());
            return udpatedTechnicianPosition;
        }

        /// <summary>
        /// Deletes a technician position.
        /// </summary>
        /// <param name="id">Id of the technician position to delete.</param>
        /// <returns>Task processing the request.</returns>
        public async Task Delete(int id)
        {
            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Client.Client_Delete";
            await connection.ExecuteAsync(storedProcedure, new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Maps a technician position from the database to its associated DTO.
        /// </summary>
        /// <param name="technicianPosition">Technician position to map.</param>
        /// <returns>Mapped technician position.</returns>
        private TechnicianPosition MapFromDB(TechnicianPositionEntity technicianPosition) => technicianPosition == null ? null : new TechnicianPosition
        {
            Id = technicianPosition.Id,
            Name = technicianPosition.Name,
            BusinessId = technicianPosition.BusinessId
        };
    }
}