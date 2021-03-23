using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using Service.Server.Models;
using Service.Server.Services.Interfaces;
using Service.Server.Services.Implementations;

namespace Service.Server.Controllers
{
    /// <summary>
    /// Handles invitation related requests.
    /// </summary>
    [Route("api/1/invitations")]
    public class InvitationsController : BaseController
    {
        private const string GENERIC_TOKEN_FAILURE = "Invitation token is expired or invalid.";

        private readonly IRequestContext _requestContext;
        private readonly IJwtService _jwtService;
        private readonly IInvitationService _invitationService;
        private readonly IUserService _userService;
        private readonly ITechnicianService _technicianService;
        private readonly IHashService _hashService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvitationsController" /> class.
        /// </summary>
        /// <param name="requestContext">Request context to use.</param>
        /// <param name="jwtService">Jwt service to use.</param>
        /// <param name="invitationService">Invitation service to use.</param>
        /// <param name="userService">User service to use.</param>
        /// <param name="technicianService">Technician service to use.</param>
        /// <param name="hashService">Hash service to use.</param>
        public InvitationsController(IRequestContext requestContext, IJwtService jwtService, IInvitationService invitationService,
            IUserService userService, ITechnicianService technicianService, IHashService hashService)
        {
            _requestContext = requestContext ?? throw new ArgumentNullException(nameof(requestContext));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _invitationService = invitationService ?? throw new ArgumentNullException(nameof(invitationService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _technicianService = technicianService ?? throw new ArgumentNullException(nameof(technicianService));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
        }

        /// <summary>
        /// Sends an invitation to be a technician.
        /// </summary>
        /// <param name="invitation">Invite to send.</param>
        /// <returns>Was the invitation sent successfully.</returns>
        [Route("technician/send")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendTechnicianInvitation([FromBody] TechnicianInvitation invitation)
        {
            if (invitation == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(invitation);
            }

            // Ensure the technician exists.
            var technician = _technicianService.Get(invitation.TechnicianId);
            if (technician == null)
            {
                return BadRequest("Technician does not exist.");
            }

            // Determine if the user is allowed to send an invitation on behalf of the business.
            var user = await _requestContext.User();
        }

        /// <summary>
        /// Processes an invitation to be a technician.
        /// </summary>
        /// <param name="signup">Details needed to sign a user up as a technician.</param>
        /// <returns>Was the invitation processed successfully.</returns>
        [Route("technician/accept")]
        [HttpPost]
        public async Task<IActionResult> AcceptTechnicianInvitation([FromBody] TechnicianSignup signup)
        {
            if (signup == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Extract the invitation id from the token.
            var id = _jwtService.Read(signup.InvitationToken);
            if (!id.HasValue)
            {
                return BadRequest(GENERIC_TOKEN_FAILURE);
            }

            // Ensure the invitation exists.
            var technicianInvitation = await _invitationService.GetTechnicianInvitation(id.Value);
            if (technicianInvitation == null)
            {
                return BadRequest(GENERIC_TOKEN_FAILURE);
            }

            // Ensure the invitation has not expired. Invitations expire after 24 hours.
            var invitationAge = TimeProvider.Current.UtcNow - technicianInvitation.InvitationDate;
            if (invitationAge.TotalHours > 24)
            {
                return BadRequest(GENERIC_TOKEN_FAILURE);
            }

            // Get the technician's details.
            var technician = await _technicianService.Get(id.Value);
            if (technician == null)
            {
                return BadRequest(GENERIC_TOKEN_FAILURE);
            }

            // If the user doesn't exist, create it.
            // Otherwise ensure the user is authorized to accept the invitation.
            var user = await _userService.Get(signup.EmailAddress);
            if (user == null)
            {
                // Hash the password.
                var salt = _hashService.GenerateSalt();
                var hashedPassword = _hashService.Hash(signup.Password, salt);

                // Prepare the user. The user inherits the business id of the technician record.
                user = new User
                {
                    EmailAddress = signup.EmailAddress,
                    NewPassword = hashedPassword,
                    BusinessId = technician.BusinessId.Value
                };

                // Create the user.
                user = await _userService.Create(user);
                if (user == null)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                // User exists. Check the password to ensure the user is who they say they are.
                if (!_hashService.Compare(user.Password, signup.Password))
                {
                    return Unauthorized();
                }

                // Ensure the user belongs to the same business as the technician.
                if (user.BusinessId.Value != technician.BusinessId.Value)
                {
                    return Forbid();
                }
            }

            // Accept the invitation.
            await _invitationService.AcceptTechnicianInvitation(technician.Id.Value, user.Id.Value);

            return Ok();
        }
    }
}
