using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;

using Service.Server.Entities;
using Service.Server.Services.Interfaces;

namespace Service.Server.Services.Implementations
{
    /// <summary>
    /// Handles invitation related requests.
    /// </summary>
    public sealed class InvitationService : IInvitationService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvitationService" /> class.
        /// </summary>
        /// <param name="connectionFactory">Database connection factory to use.</param>
        public InvitationService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /// <summary>
        /// Gets the technician invitation sent for a given technician.
        /// </summary>
        /// <param name="technicianId">Id of the technician that should have its invitation retrieved.</param>
        /// <returns>Technician invitation for the given technician.</returns>
        public async Task<TechnicianInvitation> GetTechnicianInvitation(int technicianId)
        {
            using var connection = _connectionFactory.Build();
            const string sql = "SELECT * FROM Technician.vInvitations WHERE TechnicianId = @technicianId";
            var technicianInvitations = await connection.QueryAsync<TechnicianInvitation>(sql, new { technicianId });
            var technicianInvitation = technicianInvitations.FirstOrDefault();
            return technicianInvitation;
        }

        /// <summary>
        /// Accepts a technician invitation. This will bind the user
        /// to the technician and delete the invitation.
        /// </summary>
        /// <param name="technicianId">Id of the technician being accepted.</param>
        /// <param name="userId">Id of the user accepting the technician role.</param>
        /// <returns>Result of the operation.</returns>
        public async Task AcceptTechnicianInvitation(int technicianId, int userId)
        {
            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Technician.Invitation_Accept";
            await connection.ExecuteAsync(storedProcedure, new
            {
                TechnicianId = technicianId,
                UserId = userId
            });
        }
    }
}
