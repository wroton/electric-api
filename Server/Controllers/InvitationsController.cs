using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using Service.Server.Services.Interfaces;

namespace Service.Server.Controllers
{
    /// <summary>
    /// Handles invitation related requests.
    /// </summary>
    [Authorize]
    [Route("api/1/invitations")]
    public class InvitationsController : BaseController
    {
        private readonly IInvitationService _invitationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvitationsController" /> class.
        /// </summary>
        /// <param name="invitationService">Invitation service to use.</param>
        public InvitationsController(IInvitationService invitationService)
        {
            _invitationService = invitationService ?? throw new ArgumentNullException(nameof(invitationService));
        }

        /// <summary>
        /// Processes an invitation to be a business administrator.
        /// </summary>
        /// <returns>Was the invitation processed successfully.</returns>
        [Route("administrator")]
        [HttpPost]
        public async Task<IActionResult> Administrator()
        {
            // 1. User Id + Hash

            return Ok();
        }
    }
}
