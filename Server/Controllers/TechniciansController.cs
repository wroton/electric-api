using Microsoft.AspNetCore.Authorization;
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
    /// Handles technician related requests.
    /// </summary>
    [Authorize]
    [Route("api/1/technicians")]
    public class TechniciansController : BaseController
    {
        private readonly ITechnicianService _technicianService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechniciansController" /> class.
        /// </summary>
        /// <param name="technicianService">Technician service to use.</param>
        public TechniciansController(ITechnicianService technicianService)
        {
            _technicianService = technicianService ?? throw new ArgumentNullException(nameof(technicianService));
        }

        /// <summary>
        /// Gets a list of technicians to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the technicians to which the caller has access.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<int>), 200)]
        public async Task<IActionResult> List()
        {
            var ids = await _technicianService.List();
            return Ok(ids);
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

            var technicians = await _technicianService.Resolve(ids);
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
            var technician = await _technicianService.Get(id);
            if (technician == null)
            {
                return NotFound("Tecnician could not be found.");
            }

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

            var createdTechnician = await _technicianService.Create(technician);
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

            var updatedTechnician = await _technicianService.Update(technician);
            if (updatedTechnician == null)
            {
                return NotFound("Technician could not be found.");
            }

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
            var technician = await _technicianService.Get(id);
            if (technician == null)
            {
                return NotFound("Technician could not be found.");
            }

            await _technicianService.Delete(id);

            return NoContent();
        }
    }
}
