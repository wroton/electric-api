using System;

namespace Service.Server.Entities
{
    /// <summary>
    /// An invitation for an administrator.
    /// </summary>
    public sealed class AdministratorInvitationEntity
    {
        /// <summary>
        /// Id of the administrator which the user will assume.
        /// </summary>
        public int AdministratorId { get; private set; }

        /// <summary>
        /// Invitation token sent to the user.
        /// </summary>
        public string InvitationToken { get; private set; }

        /// <summary>
        /// Date and time that the invitation was originally sent.
        /// </summary>
        public DateTime InvitationDate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministratorInvitationEntity" /> class.
        /// </summary>
        /// <param name="administratorId">Id of the administrator which the user will assume.</param>
        /// <param name="invitationToken">Invitation token sent to the user.</param>
        /// <param name="invitationDate">Date and time that the invitation was originally sent.</param>
        public AdministratorInvitationEntity(int administratorId, string invitationToken, DateTime invitationDate)
        {
            AdministratorId = administratorId;
            InvitationToken = invitationToken;
            InvitationDate = invitationDate;
        }
    }
}
