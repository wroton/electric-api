using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Service.Server.Entities;
using Service.Server.Models;
using Service.Server.Services.Interfaces;

namespace Service.Server.Controllers
{
    /// <summary>
    /// Handles technician related requests.
    /// </summary>
    [Route("api/1/technicians")]
    public class TechniciansController : BaseController
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechniciansController" /> class.
        /// </summary>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public TechniciansController(IDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _connection = connectionFactory.Build();
        }

        /// <summary>
        /// Gets a list of technicians to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the technicians to which the caller has access.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<int>), 200)]
        public async Task<IActionResult> List()
        {
            const string sql = "SELECT Id FROM Technician.vTechnicians";
            var dbIds = await _connection.QueryAsync<int>(sql);
            return Ok(dbIds);
        }

        /// <summary>
        /// Resolves a list of technicians.
        /// </summary>
        /// <param name="ids">Ids of the technicians to resolve.</param>
        /// <returns>Resolved technicians.</returns>
        [HttpPost]
        [Route("resolve")]
        [ProducesResponseType(typeof(IEnumerable<Technician>), 200)]
        public async Task<IActionResult> Resolve([FromBody] IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Ok(Array.Empty<Technician>());
            }

            var splitIds = string.Join(',', ids);

            const string storedProcedure = "Technician.Technicians_Resolve";
            var dbTechnicians = await _connection.QueryAsync<TechnicianEntity>(storedProcedure, new { ids = splitIds }, commandType: CommandType.StoredProcedure);
            var technicians = dbTechnicians.Select(MapFromDB);
            return Ok(technicians);
        }

        /// <summary>
        /// Gets a technician.
        /// </summary>
        /// <param name="id">Id of the technician to get.</param>
        /// <returns>Technician with the given id.</returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(Technician), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var dbTechnician = await GetTechnicianById(id);
            if (dbTechnician == null)
            {
                return NotFound("Technician could not be found.");
            }

            var technician = MapFromDB(dbTechnician);
            return Ok(technician);
        }

        /// <summary>
        /// Creates a technician.
        /// </summary>
        /// <param name="technician">Technician to create.</param>
        /// <returns>Created technician.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Technician), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody] Technician technician)
        {
            if (technician == null)
            {
                return BadRequest("Technician was not provided in the body or could not be interpreted as JSON.");
            }

            const string storedProcedure = "Technician.Technician_Create";
            var dbTechnicians = await _connection.QueryAsync<TechnicianEntity>(storedProcedure, new
            {
                technician.Name,
                technician.UserId
            }, commandType: CommandType.StoredProcedure);
            var createdTechnician = MapFromDB(dbTechnicians.FirstOrDefault());
            return Ok(createdTechnician);
        }

        /// <summary>
        /// Updates a technician.
        /// </summary>
        /// <param name="technician">Technician to update.</param>
        /// <returns>Updated technician.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(Technician), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Put([FromBody] Technician technician)
        {
            if (technician == null)
            {
                return BadRequest("Technician was not provided in the body or could not be interpreted as JSON.");
            }

            if (!technician.Id.HasValue)
            {
                return BadRequest("Technician id must be provided.");
            }

            var dbTechnician = await GetTechnicianById(technician.Id.Value);
            if (dbTechnician == null)
            {
                return NotFound("Technician could not be found.");
            }

            const string storedProcedure = "Technician.Technician_Update";
            var dbTechnicians = await _connection.QueryAsync<TechnicianEntity>(storedProcedure, new
            {
                technician.Id,
                technician.Name,
                technician.UserId
            }, commandType: CommandType.StoredProcedure);
            var updatedTechnician = MapFromDB(dbTechnicians.FirstOrDefault());
            return Ok(updatedTechnician);
        }

        /// <summary>
        /// Deletes a technician.
        /// </summary>
        /// <param name="id">Id of the technician to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(Technician), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var dbTechnician = await GetTechnicianById(id);
            if (dbTechnician == null)
            {
                return NotFound("Technician could not be found.");
            }

            const string storedProcedure = "Technician.Technician_Delete";
            await _connection.ExecuteAsync(storedProcedure, new { Id = id }, commandType: CommandType.StoredProcedure);
            return Ok();
        }

        /// <summary>
        /// Gets a technician by its id.
        /// </summary>
        /// <param name="id">Id of the technician to get.</param>
        /// <returns>Technician with the given id.</returns>
        private async Task<TechnicianEntity> GetTechnicianById(int id)
        {
            const string sql = "SELECT * FROM Technician.vTechnicians WHERE Id = @id";
            var dbTechnicians = await _connection.QueryAsync<TechnicianEntity>(sql, new { id });
            return dbTechnicians.FirstOrDefault();
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
            UserId = technician.UserId
        };
    }
}
