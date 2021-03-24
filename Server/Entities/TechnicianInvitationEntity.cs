using System;

namespace Service.Server.Entities
{
    /// <summary>
    /// An invitation for a technician.
    /// </summary>
    public sealed class TechnicianInvitationEntity
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

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicianInvitationEntity" /> class.
        /// </summary>
        /// <param name="technicianId">Id of the technician which the user will assume.</param>
        /// <param name="invitationToken">Invitation token sent to the user.</param>
        /// <param name="invitationDate">Date and time that the invitation was originally sent.</param>
        public TechnicianInvitationEntity(int technicianId, string invitationToken, DateTime invitationDate)
        {
            TechnicianId = technicianId;
            InvitationToken = invitationToken;
            InvitationDate = invitationDate;
        }
    }
}
