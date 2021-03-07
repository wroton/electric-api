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
    /// Handles technician position related requests.
    /// </summary>
    [Route("api/1/technicianpositions")]
    public class TechnicianPositionsController : BaseController
    {
        private readonly ITechnicianPositionService _technicianPositionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicianPositionsController" /> class.
        /// </summary>
        /// <param name="technicianPositionService">Technician position service to use.</param>
        public TechnicianPositionsController(ITechnicianPositionService technicianPositionService)
        {
            _technicianPositionService = technicianPositionService ?? throw new ArgumentNullException(nameof(technicianPositionService));
        }

        /// <summary>
        /// Gets a list of technician positions to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the technician positions to which the caller has access.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<int>), 200)]
        public async Task<IActionResult> List()
        {
            var ids = await _technicianPositionService.List();
            return Ok(ids);
        }

        /// <summary>
        /// Resolves a list of technician positions.
        /// </summary>
        /// <param name="ids">Ids of the technician positions to resolve.</param>
        /// <returns>Resolved technician positions.</returns>
        [HttpPost]
        [Route("resolve")]
        [ProducesResponseType(typeof(IEnumerable<TechnicianPosition>), 200)]
        public async Task<IActionResult> Resolve([FromBody] IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Ok(Array.Empty<TechnicianPosition>());
            }

            var technicianPositions = await _technicianPositionService.Resolve(ids);
            return Ok(technicianPositions);
        }

        /// <summary>
        /// Gets a technician position.
        /// </summary>
        /// <param name="id">Id of the technician position to get.</param>
        /// <returns>Technician position with the given id.</returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(TechnicianPosition), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var technicianPosition = await _technicianPositionService.Get(id);
            if (technicianPosition == null)
            {
                return NotFound("Technician position could not be found.");
            }

            return Ok(technicianPosition);
        }

        /// <summary>
        /// Creates a technician position.
        /// </summary>
        /// <param name="technicianPosition">Technician position to create.</param>
        /// <returns>Created technician position.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TechnicianPosition), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody] TechnicianPosition technicianPosition)
        {
            if (technicianPosition == null)
            {
                return BadRequest("Technician position was not provided in the body or could not be interpreted as JSON.");
            }

            var createdTechnicianPosition = await _technicianPositionService.Create(technicianPosition);
            return Ok(createdTechnicianPosition);
        }

        /// <summary>
        /// Updates a technician position.
        /// </summary>
        /// <param name="technicianPosition">Technician position to update.</param>
        /// <returns>Updated technician position.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(TechnicianPosition), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Put([FromBody] TechnicianPosition technicianPosition)
        {
            if (technicianPosition == null)
            {
                return BadRequest("Technician position was not provided in the body or could not be interpreted as JSON.");
            }

            if (!technicianPosition.Id.HasValue)
            {
                return BadRequest("Technician position id must be provided.");
            }

            var updatedTechnicianPosition = await _technicianPositionService.Update(technicianPosition);
            if (updatedTechnicianPosition == null)
            {
                return NotFound("Technician position could not be found.");
            }

            return Ok(updatedTechnicianPosition);
        }

        /// <summary>
        /// Deletes a technician position.
        /// </summary>
        /// <param name="id">Id of the technician position to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(TechnicianPosition), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var technicianPosition = await _technicianPositionService.Get(id);
            if (technicianPosition == null)
            {
                return NotFound("Technician position could not be found.");
            }

            await _technicianPositionService.Delete(id);

            return NoContent();
        }
    }
}
