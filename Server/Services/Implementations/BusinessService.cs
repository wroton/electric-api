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
    /// Handles business related requests.
    /// </summary>
    public sealed class BusinessService : IBusinessService
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessService" /> class.
        /// </summary>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public BusinessService(IDbConnectionFactory connectionFactory)
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
        public async Task<IEnumerable<int>> List()
        {
            const string sql = "SELECT Id FROM Business.vBusinesses";
            var dbIds = await _connection.QueryAsync<int>(sql);
            return dbIds;
        }

        /// <summary>
        /// Resolves a list of businesses.
        /// </summary>
        /// <param name="ids">Ids of the businesses to resolve.</param>
        /// <returns>Resolved businesses.</returns>
        public async Task<IEnumerable<Business>> Resolve(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentException("Cannot be null or have a length of zero.", nameof(ids));
            }

            var splitIds = string.Join(',', ids);

            const string storedProcedure = "Business.Businesses_Resolve";
            var dbBusinesss = await _connection.QueryAsync<BusinessEntity>(storedProcedure, new { ids = splitIds }, commandType: CommandType.StoredProcedure);
            var businesses = dbBusinesss.Select(MapFromDB);
            return businesses;
        }

        /// <summary>
        /// Gets a business.
        /// </summary>
        /// <param name="id">Id of the business to get.</param>
        /// <returns>Business with the given id.</returns>
        public async Task<Business> Get(int id)
        {
            const string sql = "SELECT * FROM Business.vBusinesss WHERE Id = @id";
            var dbBusinesss = await _connection.QueryAsync<BusinessEntity>(sql, new { id });
            var business = MapFromDB(dbBusinesss.SingleOrDefault());
            return business;
        }

        /// <summary>
        /// Creates a business.
        /// </summary>
        /// <param name="business">Business to create.</param>
        /// <returns>Created business.</returns>
        public async Task<Business> Create(Business business)
        {
            if (business == null)
            {
                throw new ArgumentNullException(nameof(business));
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
            return createdBusiness;
        }

        /// <summary>
        /// Updates a business.
        /// </summary>
        /// <param name="business">Business to update.</param>
        /// <returns>Updated business.</returns>
        public async Task<Business> Update(Business business)
        {
            if (business == null)
            {
                throw new ArgumentNullException(nameof(business));
            }

            if (!business.Id.HasValue)
            {
                throw new ArgumentException("Id must be provided.", nameof(business));
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
            return updatedBusiness;
        }

        /// <summary>
        /// Deletes a business.
        /// </summary>
        /// <param name="id">Id of the business to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        public async Task Delete(int id)
        {
            const string storedProcedure = "Business.Business_Delete";
            await _connection.ExecuteAsync(storedProcedure, new { Id = id }, commandType: CommandType.StoredProcedure);
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
