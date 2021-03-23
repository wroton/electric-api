using System;

namespace Service.Server.Entities
{
    /// <summary>
    /// An invitation for a technician.
    /// </summary>
    public sealed class TechnicianInvitation
    {
        /// <summary>
        /// Id of the technician which the user will assume.
        /// </summary>
        public int TechnicianId { get; private set; }

        /// <summary>
        /// Invitation token sent to the user.
        /// </summary>
        public string InvitationToken { get; private set; }

        /// <summary>
        /// Date and time that the invitation was originally sent.
        /// </summary>
        public DateTime InvitationDate { get; private set; }
    }
}
