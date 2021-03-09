using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using Service.Server.Models;
using Service.Server.Services.Implementations;
using Service.Server.Services.Interfaces;

namespace Service.Server.Controllers
{
    /// <summary>
    /// Processes token related requests.
    /// </summary>
    [Route("api/1/token")]
    public sealed class TokenController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IHashService _hashService;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenController" /> class.
        /// </summary>
        /// <param name="userService">User service to use.</param>
        /// <param name="hashService">Hash service to use.</param>
        /// <param name="jwtService">Jwt service to use.</param>
        public TokenController(IUserService userService, IHashService hashService, IJwtService jwtService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        /// <summary>
        /// Gets the user currently authenticated.
        /// </summary>
        /// <returns>User currently authenticated.</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        /// <summary>
        /// Creates an authorization token using the authentication details.
        /// </summary>
        /// <param name="authenticationDetails">Authentication details used to authenticate the user.</param>
        /// <returns>Authorization token.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody] AuthenticationDetails authenticationDetails)
        {
            if (authenticationDetails == null)
            {
                return BadRequest();
            }

            // Get the user.
            var dbUser = await _userService.Get(authenticationDetails.Username);
            if (dbUser == null)
            {
                return Unauthorized();
            }

            // Ensure the password is valid.
            if (!_hashService.Compare(dbUser.Password, authenticationDetails.Password))
            {
                return Unauthorized();
            }

            // Create the token.
            var token = _jwtService.Token(dbUser.Id.Value);

            // Create a cookie to be used for authorization.
            Response.Cookies.Append("Authorization", token, new CookieOptions
            {
                Expires = new DateTimeOffset(TimeProvider.Current.Now.AddMinutes(15)),
                HttpOnly = true,
                Secure = false // Setup configuration to switch this if in production.
            });

            return Ok(token);
        }
    }
}
