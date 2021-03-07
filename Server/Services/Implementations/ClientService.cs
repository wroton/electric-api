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
    /// Handles client related requests.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService" /> class.
        /// </summary>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public ClientService(IDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _connection = connectionFactory.Build();
        }

        /// <summary>
        /// Gets a list of clients to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the clients to which the caller has access.</returns>
        public async Task<IEnumerable<int>> List()
        {
            const string sql = "SELECT Id FROM Client.vClients";
            var dbIds = await _connection.QueryAsync<int>(sql);
            return dbIds;
        }

        /// <summary>
        /// Resolves a list of clients.
        /// </summary>
        /// <param name="ids">Ids of the clients to resolve.</param>
        /// <returns>Resolved clients.</returns>
        public async Task<IEnumerable<Client>> Resolve(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentException("Cannot be null or have a length of zero.", nameof(ids));
            }

            var splitIds = string.Join(',', ids);

            const string storedProcedure = "Client.Clients_Resolve";
            var dbClients = await _connection.QueryAsync<ClientEntity>(storedProcedure, new { ids = splitIds }, commandType: CommandType.StoredProcedure);
            var clients = dbClients.Select(MapFromDB);
            return clients;
        }

        /// <summary>
        /// Gets a client.
        /// </summary>
        /// <param name="id">Id of the client to get.</param>
        /// <returns>Client with the given id.</returns>
        public async Task<Client> Get(int id)
        {
            const string sql = "SELECT * FROM Client.vClients WHERE Id = @id";
            var dbClients = await _connection.QueryAsync<ClientEntity>(sql, new { id });
            var client = MapFromDB(dbClients.SingleOrDefault());
            return client;
        }

        /// <summary>
        /// Creates a client.
        /// </summary>
        /// <param name="client">Client to create.</param>
        /// <returns>Created client.</returns>
        public async Task<Client> Create(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            const string storedProcedure = "Client.Client_Create";
            var dbClients = await _connection.QueryAsync<ClientEntity>(storedProcedure, new
            {
                client.Name,
                client.AddressLine1,
                client.AddressLine2,
                client.City,
                client.State,
                client.ZipCode
            }, commandType: CommandType.StoredProcedure);
            var createdClient = MapFromDB(dbClients.FirstOrDefault());
            return createdClient;
        }

        /// <summary>
        /// Updates a client.
        /// </summary>
        /// <param name="client">Client to update.</param>
        /// <returns>Updated client.</returns>
        public async Task<Client> Update(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (!client.Id.HasValue)
            {
                throw new ArgumentException("Id must be provided.", nameof(client));
            }

            const string storedProcedure = "Client.Client_Update";
            var dbClients = await _connection.QueryAsync<ClientEntity>(storedProcedure, new
            {
                client.Id,
                client.Name,
                client.AddressLine1,
                client.AddressLine2,
                client.City,
                client.State,
                client.ZipCode
            }, commandType: CommandType.StoredProcedure);
            var updatedClient = MapFromDB(dbClients.FirstOrDefault());
            return updatedClient;
        }

        /// <summary>
        /// Deletes a client.
        /// </summary>
        /// <param name="id">Id of the client to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        public async Task Delete(int id)
        {
            const string storedProcedure = "Client.Client_Delete";
            await _connection.ExecuteAsync(storedProcedure, new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Maps a client from the database to its associated DTO.
        /// </summary>
        /// <param name="client">Client to map.</param>
        /// <returns>Mapped client.</returns>
        private Client MapFromDB(ClientEntity client) => client == null ? null : new Client
        {
            Id = client.Id,
            Name = client.Name,
            AddressLine1 = client.AddressLine1,
            AddressLine2 = client.AddressLine2,
            City = client.City,
            State = client.State,
            ZipCode = client.ZipCode
        };
    }
}
