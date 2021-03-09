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
    /// Handles user related requests.
    /// </summary>
    [Authorize]
    [Route("api/1/users")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController" /> class.
        /// </summary>
        /// <param name="userService">User service to use.</param>
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Resolves a list of users.
        /// </summary>
        /// <param name="ids">Ids of the users to resolve.</param>
        /// <returns>Resolved users.</returns>
        [HttpPost]
        [Route("resolve")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> Resolve([FromBody] IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Ok(Array.Empty<User>());
            }

            var users = await _userService.Resolve(ids);
            return Ok(users);
        }

        /// <summary>
        /// Gets a user.
        /// </summary>
        /// <param name="id">Id of the user to get.</param>
        /// <returns>User with the given id.</returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var user = await _userService.Get(id);
            if (user == null)
            {
                return NotFound("User could not be found.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="user">User to create.</param>
        /// <returns>Created user.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            // Ensure the username isn't already taken.
            var existingUser = await _userService.Get(user.Username);
            if (existingUser != null)
            {
                return Conflict("The username provided is unavailable.");
            }

            // Override the business id with the one of the calling user.
            user.BusinessId = null;

            // Create the user.
            var createdUser = await _userService.Create(user);
            return Ok(createdUser);
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user">User to update.</param>
        /// <returns>Updated user.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User was not provided in the body or could not be interpreted as JSON.");
            }

            if (!user.Id.HasValue)
            {
                return BadRequest("User id must be provided.");
            }

            var updatedUser = await _userService.Update(user);
            if (updatedUser == null)
            {
                return NotFound("User could not be found.");
            }

            return Ok(updatedUser);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">Id of the user to delete.</param>
        /// <returns>Status code indicating the result of the request.</returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var user = await _userService.Get(id);
            if (user == null)
            {
                return NotFound("User could not be found.");
            }

            await _userService.Delete(id);

            return NoContent();
        }
    }
}
