using System.Threading.Tasks;

using Service.Server.Entities;

namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Handles invitation related requests.
    /// </summary>
    public interface IInvitationService
    {
        /// <summary>
        /// Gets the administrator invitation sent for a given administrator.
        /// </summary>
        /// <param name="administratorId">Id of the administrator that should have its invitation retrieved.</param>
        /// <returns>Administrator invitation for the given administrator.</returns>
        Task<AdministratorInvitationEntity> GetAdministratorInvitation(int administratorId);

        /// <summary>
        /// Creates a administrator invitation.
        /// </summary>
        /// <param name="invitation">Invitation to create.</param>
        /// <returns>Created administrator invitation.</returns>
        Task<AdministratorInvitationEntity> CreateAdministratorInvitation(AdministratorInvitationEntity invitation);

        /// <summary>
        /// Accepts an administrator invitation. This will bind the user
        /// to the administrator and delete the invitation.
        /// </summary>
        /// <param name="administratorId">Id of the administrator being accepted.</param>
        /// <param name="userId">Id of the user accepting the administrator role.</param>
        /// <returns>Result of the operation.</returns>
        Task AcceptAdministratorInvitation(int administratorId, int userId);

        /// <summary>
        /// Gets the technician invitation sent for a given technician.
        /// </summary>
        /// <param name="technicianId">Id of the technician that should have its invitation retrieved.</param>
        /// <returns>Technician invitation for the given technician.</returns>
        Task<TechnicianInvitationEntity> GetTechnicianInvitation(int technicianId);

        /// <summary>
        /// Creates a technician invitation.
        /// </summary>
        /// <param name="invitation">Invitation to create.</param>
        /// <returns>Created technician invitation.</returns>
        Task<TechnicianInvitationEntity> CreateTechnicianInvitation(TechnicianInvitationEntity invitation);

        /// <summary>
        /// Accepts a technician invitation. This will bind the user
        /// to the technician and delete the invitation.
        /// </summary>
        /// <param name="technicianId">Id of the technician being accepted.</param>
        /// <param name="userId">Id of the user accepting the technician role.</param>
        /// <returns>Result of the operation.</returns>
        Task AcceptTechnicianInvitation(int technicianId, int userId);
    }
}
