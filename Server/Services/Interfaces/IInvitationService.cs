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
        /// Gets the technician invitation sent for a given technician.
        /// </summary>
        /// <param name="technicianId">Id of the technician that should have its invitation retrieved.</param>
        /// <returns>Technician invitation for the given technician.</returns>
        Task<TechnicianInvitation> GetTechnicianInvitation(int technicianId);

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
