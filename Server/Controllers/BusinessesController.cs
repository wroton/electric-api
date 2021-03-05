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
    /// Handles business related requests.
    /// </summary>
    [Route("api/1/businesses")]
    public class BusinessesController : BaseController
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessesController" /> class.
        /// </summary>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public BusinessesController(IDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _connection = connectionFactory.Build();
        }

        /// <summary>
        /// Gets a list of businesses to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the businesses to which the caller has access.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<int>), 200)]
        public async Task<IActionResult> List()
        {
            const string sql = "SELECT Id FROM Business.vBusinesses";
            var dbIds = await _connection.QueryAsync<int>(sql);
            return Ok(dbIds);
        }

        /// <summary>
        /// Resolves a list of businesses.
        /// </summary>
        /// <param name="ids">Ids of the businesses to resolve.</param>
        /// <returns>Resolved businesses.</returns>
        [HttpPost]
        [Route("resolve")]
        [ProducesResponseType(typeof(IEnumerable<Business>), 200)]
        public async Task<IActionResult> Resolve([FromBody] IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Ok(Array.Empty<Business>());
            }

            var splitIds = string.Join(',', ids);

            const string storedProcedure = "Business.Businesses_Resolve";
            var dbBusinesss = await _connection.QueryAsync<BusinessEntity>(storedProcedure, new { ids = splitIds }, commandType: CommandType.StoredProcedure);
            var businesss = dbBusinesss.Select(MapFromDB);
            return Ok(businesss);
        }

        /// <summary>
        /// Gets a business.
        /// </summary>
        /// <param name="id">Id of the business to get.</param>
        /// <returns>Business with the given id.</returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(Business), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var dbBusiness = await GetBusinessById(id);
            if (dbBusiness == null)
            {
                return NotFound("Business could not be found.");
            }

            var business = MapFromDB(dbBusiness);
            return Ok(business);
        }

        /// <summary>
        /// Creates a business.
        /// </summary>
        /// <param name="business">Business to create.</param>
        /// <returns>Created business.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Business), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody] Business business)
        {
            if (business == null)
            {
                return BadRequest("Business was not provided in the body or could not be interpreted as JSON.");
            }

            const string storedProcedure = "Business.Business_Create";
            var dbBusinesses = await _connection.QueryAsync<BusinessEntity>(storedProcedure, new
            {
                business.Name,
                business.AddressLine1,
                business.AddressLine2,
                business.City,
                business.State,
                business.ZipCode
            }, commandType: CommandType.StoredProcedure);
            var createdBusiness = MapFromDB(dbBusinesses.FirstOrDefault());
            return Ok(createdBusiness);
        }

        /// <summary>
        /// Updates a business.
        /// </summary>
        /// <param name="business">Business to update.</param>
        /// <returns>Updated business.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(Business), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Put([FromBody] Business business)
        {
            if (business == null)
            {
                return BadRequest("Business was not provided in the body or could not be interpreted as JSON.");
            }

            if (!business.Id.HasValue)
            {
                return BadRequest("Business id must be provided.");
            }

            var dbBusiness = await GetBusinessById(business.Id.Value);
            if (dbBusiness == null)
            {
                return NotFound("Buisness could not be found.");
            }

            const string storedProcedure = "Business.Business_Update";
            var dbBusinesses = await _connection.QueryAsync<BusinessEntity>(storedProcedure, new
            {
                business.Id,
                business.Name,
                business.AddressLine1,
                business.AddressLine2,
                business.City,
                business.State,
                business.ZipCode
            }, commandType: CommandType.StoredProcedure);
            var updatedBusiness = MapFromDB(dbBusinesses.FirstOrDefault());
            return Ok(updatedBusiness);
        }

        /// <summary>
        /// Deletes a business.
        /// </summary>
        /// <param name="id">Id of the business to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(Business), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var dbBusiness = await GetBusinessById(id);
            if (dbBusiness == null)
            {
                return NotFound("Business could not be found.");
            }

            const string storedProcedure = "Business.Business_Delete";
            await _connection.ExecuteAsync(storedProcedure, new { Id = id }, commandType: CommandType.StoredProcedure);
            return Ok();
        }

        /// <summary>
        /// Gets a business by its id.
        /// </summary>
        /// <param name="id">Id of the business to get.</param>
        /// <returns>Business with the given id.</returns>
        private async Task<BusinessEntity> GetBusinessById(int id)
        {
            const string sql = "SELECT * FROM Business.vBusinesss WHERE Id = @id";
            var dbBusinesss = await _connection.QueryAsync<BusinessEntity>(sql, new { id });
            return dbBusinesss.FirstOrDefault();
        }

        /// <summary>
        /// Maps a business from the database to its associated DTO.
        /// </summary>
        /// <param name="business">Business to map.</param>
        /// <returns>Mapped business.</returns>
        private Business MapFromDB(BusinessEntity business) => business == null ? null : new Business
        {
            Id = business.Id,
            Name = business.Name,
            AddressLine1 = business.AddressLine1,
            AddressLine2 = business.AddressLine2,
            City = business.City,
            State = business.State,
            ZipCode = business.ZipCode
        };
    }
}
