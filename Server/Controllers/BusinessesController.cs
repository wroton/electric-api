using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Service.Server.Models;
using Service.Server.Services.Interfaces;

namespace Service.Server.Controllers
{
    /// <summary>
    /// Handles business related requests.
    /// </summary>
    [Authorize]
    [Route("api/1/businesses")]
    public class BusinessesController : BaseController
    {
        private readonly IBusinessService _businessService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessesController" /> class.
        /// </summary>
        /// <param name="businessService">Business service to use.</param>
        public BusinessesController(IBusinessService businessService)
        {
            _businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
        }

        /// <summary>
        /// Gets a list of businesses to which the caller has access.
        /// </summary>
        /// <returns>List of ids of the businesses to which the caller has access.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<int>), 200)]
        public async Task<IActionResult> List()
        {
            var ids = await _businessService.List();
            return Ok(ids);
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
            if (!ids.Any())
            {
                return Ok(Array.Empty<Business>());
            }
            var businesses = await _businessService.Resolve(ids);
            return Ok(businesses);
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
            var business = await _businessService.Get(id);
            if (business == null)
            {
                return NotFound("Business could not be found.");
            }

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
            var createdBusiness = await _businessService.Create(business);
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
            if (!business.Id.HasValue)
            {
                return BadRequest("Business id must be provided.");
            }

            var updatedBusiness = await _businessService.Update(business);
            if (updatedBusiness == null)
            {
                return NotFound("Business could not be found.");
            }

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
            var business = await _businessService.Get(id);
            if (business == null)
            {
                return NotFound("Business could not be found.");
            }

            await _businessService.Delete(id);

            return NoContent();
        }
    }
}
