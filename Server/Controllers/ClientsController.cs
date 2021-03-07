using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Service.Server.Models;
using Service.Server.Services.Interfaces;

namespace Service.Server.Controllers
{
    /// <summary>
    /// Handles client related requests.
    /// </summary>
    [Route("api/1/clients")]
    public class ClientsController : BaseController
    {
        private readonly IClientService _clientService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientsController" /> class.
        /// </summary>
        /// <param name="clientService">Client service to use.</param>
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        /// <summary>
        /// Gets a list of clients to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the clients to which the caller has access.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<int>), 200)]
        public async Task<IActionResult> List()
        {
            var ids = await _clientService.List();
            return Ok(ids);
        }

        /// <summary>
        /// Resolves a list of clients.
        /// </summary>
        /// <param name="ids">Ids of the clients to resolve.</param>
        /// <returns>Resolved clients.</returns>
        [HttpPost]
        [Route("resolve")]
        [ProducesResponseType(typeof(IEnumerable<Client>), 200)]
        public async Task<IActionResult> Resolve([FromBody] IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Ok(Array.Empty<Client>());
            }

            var clients = await _clientService.Resolve(ids);
            return Ok(clients);
        }

        /// <summary>
        /// Gets a client.
        /// </summary>
        /// <param name="id">Id of the client to get.</param>
        /// <returns>Client with the given id.</returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var client = await _clientService.Get(id);
            if (client == null)
            {
                return NotFound("Client could not be found.");
            }

            return Ok(client);
        }

        /// <summary>
        /// Creates a client.
        /// </summary>
        /// <param name="client">Client to create.</param>
        /// <returns>Created client.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest("Client was not provided in the body or could not be interpreted as JSON.");
            }

            var createdClient = await _clientService.Create(client);
            return Ok(createdClient);
        }

        /// <summary>
        /// Updates a client.
        /// </summary>
        /// <param name="client">Client to update.</param>
        /// <returns>Updated client.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Put([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest("Client was not provided in the body or could not be interpreted as JSON.");
            }

            if (!client.Id.HasValue)
            {
                return BadRequest("Client id must be provided.");
            }

            var updatedClient = await _clientService.Update(client);
            if (updatedClient == null)
            {
                return NotFound("Client could not be found.");
            }

            return Ok(updatedClient);
        }

        /// <summary>
        /// Deletes a client.
        /// </summary>
        /// <param name="id">Id of the client to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var client = await _clientService.Get(id);
            if (client == null)
            {
                return NotFound("Client could not be found.");
            }

            await _clientService.Delete(id);

            return NoContent();
        }
    }
}
