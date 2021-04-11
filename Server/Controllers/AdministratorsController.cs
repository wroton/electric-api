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
    /// Handles administrator related requests.
    /// </summary>
    [Authorize]
    [Route("api/1/administrators")]
    public class AdministratorsController : BaseController
    {
        private readonly IRequestContext _requestContext;
        private readonly IBusinessService _businessService;
        private readonly IAdministratorService _administratorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministratorsController" /> class.
        /// </summary>
        /// <param name="requestContext">Request context to use.</param>
        /// <param name="businessService">Business service to use.</param>
        /// <param name="administratorService">Administrator service to use.</param>
        public AdministratorsController(IRequestContext requestContext, IBusinessService businessService, IAdministratorService administratorService)
        {
            _requestContext = requestContext ?? throw new ArgumentNullException(nameof(requestContext));
            _businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
            _administratorService = administratorService ?? throw new ArgumentNullException(nameof(administratorService));
        }

        /// <summary>
        /// Searches a list of administrators to which the caller has access.
        /// </summary>
        /// <param name="searchCriteria">Criteria by which the search should be performed.</param>
        /// <returns>List of ids of the administrators.</returns>
        [HttpPost]
        [Route("search")]
        [ProducesResponseType(typeof(IEnumerable<int>), 200)]
        public async Task<IActionResult> Search([FromBody] AdministratorSearch searchCriteria)
        {
            var user = await _requestContext.User();
            var ids = await _administratorService.Search(user.Id, searchCriteria);
            return Ok(ids);
        }

        /// <summary>
        /// Resolves a list of administrators.
        /// </summary>
        /// <param name="ids">Ids of the administrators to resolve.</param>
        /// <returns>Resolved administrators.</returns>
        [HttpPost]
        [Route("resolve")]
        [ProducesResponseType(typeof(IEnumerable<Administrator>), 200)]
        public async Task<IActionResult> Resolve([FromBody] IEnumerable<int> ids)
        {
            if (!ids.Any())
            {
                return Ok(Array.Empty<Technician>());
            }

            var user = await _requestContext.User();
            var administrators = await _administratorService.Resolve(user.Id, ids);
            return Ok(administrators);
        }

        /// <summary>
        /// Gets an administrator.
        /// </summary>
        /// <param name="id">Id of the administrator to get.</param>
        /// <returns>Administrator with the given id.</returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(Administrator), 200)]
        [ProducesResponseType(typeof(void), 403)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            // Get the administrator.
            var administrator = await _administratorService.Get(id);
            if (administrator == null)
            {
                return NotFound();
            }

            // Get the user and ensure the user can access the administrator.
            var user = await _requestContext.User();
            if (administrator.BusinessId != user.BusinessId && !user.SystemAdministrator)
            {
                return Forbid();
            }

            return Ok(administrator);
        }

        /// <summary>
        /// Creates an administrator.
        /// </summary>
        /// <param name="administrator">Administrator to create.</param>
        /// <returns>Created technician.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Administrator), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody] Administrator administrator)
        {
            // Get the user.
            var user = await _requestContext.User();

            // If a business id wasn't provided, use the user's business.
            var businessId = administrator.BusinessId ?? user.BusinessId;

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

            // Create the administrator.
            var createdAdministrator = await _administratorService.Create(administrator);
            return Ok(createdAdministrator);
        }

        /// <summary>
        /// Updates an administrator.
        /// </summary>
        /// <param name="administrator">Administrator to update.</param>
        /// <returns>Updated administrator.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(Administrator), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(void), 403)]
        [ProducesResponseType(typeof(void), 404)]
        [ProducesResponseType(typeof(string), 409)]
        public async Task<IActionResult> Put([FromBody] Administrator administrator)
        {
            if (!administrator.Id.HasValue)
            {
                return BadRequest("Administrator id must be provided.");
            }

            // Get the user.
            var user = await _requestContext.User();

            // Update the administrator.
            var updatedAdministrator = await _administratorService.Update(administrator);
            if (updatedAdministrator == null)
            {
                return NotFound();
            }

            return Ok(updatedAdministrator);
        }

        /// <summary>
        /// Deletes an administrator.
        /// </summary>
        /// <param name="id">Id of the administrator to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(Technician), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var administrator = await _administratorService.Get(id);
            if (administrator == null)
            {
                return NotFound("Administrator could not be found.");
            }

            // Get the user and ensure the user can access the administrator.
            var user = await _requestContext.User();
            if (administrator.BusinessId != user.BusinessId && !user.SystemAdministrator)
            {
                return Forbid();
            }

            await _administratorService.Delete(id);

            return NoContent();
        }
    }
}
