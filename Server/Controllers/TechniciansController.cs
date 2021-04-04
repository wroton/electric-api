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
        private readonly IRequestContext _requestContext;
        private readonly IBusinessService _businessService;
        private readonly ITechnicianPositionService _technicianPositionService;
        private readonly ITechnicianService _technicianService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechniciansController" /> class.
        /// </summary>
        /// <param name="requestContext">Request context to use.</param>
        /// <param name="businessService">Business service to use.</param>
        /// <param name="technicianPositionService">Technician position service to use.</param>
        /// <param name="technicianService">Technician service to use.</param>
        public TechniciansController(IRequestContext requestContext, IBusinessService businessService,
            ITechnicianPositionService technicianPositionService, ITechnicianService technicianService)
        {
            _requestContext = requestContext ?? throw new ArgumentNullException(nameof(requestContext));
            _businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
            _technicianPositionService = technicianPositionService ?? throw new ArgumentNullException(nameof(technicianPositionService));
            _technicianService = technicianService ?? throw new ArgumentNullException(nameof(technicianService));
        }

        /// <summary>
        /// Searches a list of technicians to which the caller has access.
        /// </summary>
        /// <param name="searchCriteria">Criteria by which the search should be performed.</param>
        /// <returns>List of ids of the technicians.</returns>
        [HttpPost]
        [Route("search")]
        [ProducesResponseType(typeof(IEnumerable<int>), 200)]
        public async Task<IActionResult> Search([FromBody] TechnicianSearch searchCriteria)
        {
            if (searchCriteria == null)
            {
                return BadRequest("Search criteria was not provided in the body or could not be interpreted as JSON.");
            }

            var user = await _requestContext.User();
            var ids = await _technicianService.Search(user.Id, searchCriteria);
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

            var user = await _requestContext.User();
            var technicians = await _technicianService.Resolve(user.Id, ids);
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
        [ProducesResponseType(typeof(void), 403)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            // Get the technician.
            var technician = await _technicianService.Get(id);
            if (technician == null)
            {
                return NotFound();
            }

            // Get the user and ensure the user can access the technician.
            var user = await _requestContext.User();
            if (technician.BusinessId != user.BusinessId && !user.SystemAdministrator)
            {
                return Forbid();
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

            // Get the user.
            var user = await _requestContext.User();

            // If a business id wasn't provided, use the user's business.
            var businessId = technician.BusinessId ?? user.BusinessId;

            // A business must be set.
            if (!businessId.HasValue)
            {
                return BadRequest("A business id must be provided.");
            }

            // Ensure the user has access to the business.
            if (user.BusinessId != businessId && !user.SystemAdministrator)
            {
                return Forbid();
            }

            // Ensure the business exists.
            var business = await _businessService.Get(businessId.Value);
            if (business == null)
            {
                return Conflict("Business specified does not exist.");
            }

            // Ensure the technician position exists.
            var position = await _technicianPositionService.Get(technician.PositionId);
            if (position == null)
            {
                return Conflict("Position specified does not exist.");
            }

            // Ensure the user has access to the position.
            if (user.BusinessId != position.BusinessId && !user.SystemAdministrator)
            {
                return Forbid();
            }

            // Create the technician.
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
        [ProducesResponseType(typeof(void), 403)]
        [ProducesResponseType(typeof(void), 404)]
        [ProducesResponseType(typeof(string), 409)]
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

            // Get the user.
            var user = await _requestContext.User();

            // Ensure the technician position exists.
            var position = await _technicianPositionService.Get(technician.PositionId);
            if (position == null)
            {
                return Conflict("Position specified does not exist.");
            }

            // Ensure the user has access to the position.
            if (user.BusinessId != position.BusinessId && !user.SystemAdministrator)
            {
                return Forbid();
            }

            // Update the technician.
            var updatedTechnician = await _technicianService.Update(technician);
            if (updatedTechnician == null)
            {
                return NotFound();
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

            // Get the user and ensure the user can access the technician.
            var user = await _requestContext.User();
            if (technician.BusinessId != user.BusinessId && !user.SystemAdministrator)
            {
                return Forbid();
            }

            await _technicianService.Delete(id);

            return NoContent();
        }
    }
}
