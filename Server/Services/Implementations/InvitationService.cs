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
        /// Gets the administrator invitation sent for a given administrator.
        /// </summary>
        /// <param name="administratorId">Id of the administrator that should have its invitation retrieved.</param>
        /// <returns>Administrator invitation for the given administrator.</returns>
        public async Task<AdministratorInvitationEntity> GetAdministratorInvitation(int administratorId)
        {
            using var connection = _connectionFactory.Build();
            const string sql = "SELECT * FROM Administrator.vInvitations WHERE AdministratorId = @administratorId";
            var administratorInvitations = await connection.QueryAsync<AdministratorInvitationEntity>(sql, new { administratorId });
            var administratorInvitation = administratorInvitations.FirstOrDefault();
            return administratorInvitation;
        }

        /// <summary>
        /// Creates a administrator invitation.
        /// </summary>
        /// <param name="invitation">Invitation to create.</param>
        /// <returns>Created administrator invitation.</returns>
        public async Task<AdministratorInvitationEntity> CreateAdministratorInvitation(AdministratorInvitationEntity invitation)
        {
            if (invitation == null)
            {
                throw new ArgumentNullException(nameof(invitation));
            }

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Administrator.Invitation_Create";
            var administratorInvitations = await connection.QueryAsync<AdministratorInvitationEntity>(storedProcedure, new
            {
                invitation.AdministratorId,
                invitation.InvitationToken,
                invitation.InvitationDate
            });
            var administratorInvitation = administratorInvitations.FirstOrDefault();
            return administratorInvitation;
        }

        /// <summary>
        /// Accepts an administrator invitation. This will bind the user
        /// to the administrator and delete the invitation.
        /// </summary>
        /// <param name="administratorId">Id of the administrator being accepted.</param>
        /// <param name="userId">Id of the user accepting the administrator role.</param>
        /// <returns>Result of the operation.</returns>
        public async Task AcceptAdministratorInvitation(int administratorId, int userId)
        {
            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Administrator.Invitation_Accept";
            await connection.ExecuteAsync(storedProcedure, new
            {
                AdministatorId = administratorId,
                UserId = userId
            });
        }

        /// <summary>
        /// Gets the technician invitation sent for a given technician.
        /// </summary>
        /// <param name="technicianId">Id of the technician that should have its invitation retrieved.</param>
        /// <returns>Technician invitation for the given technician.</returns>
        public async Task<TechnicianInvitationEntity> GetTechnicianInvitation(int technicianId)
        {
            using var connection = _connectionFactory.Build();
            const string sql = "SELECT * FROM Technician.vInvitations WHERE TechnicianId = @technicianId";
            var technicianInvitations = await connection.QueryAsync<TechnicianInvitationEntity>(sql, new { technicianId });
            var technicianInvitation = technicianInvitations.FirstOrDefault();
            return technicianInvitation;
        }

        /// <summary>
        /// Creates a technician invitation.
        /// </summary>
        /// <param name="invitation">Invitation to create.</param>
        /// <returns>Created technician invitation.</returns>
        public async Task<TechnicianInvitationEntity> CreateTechnicianInvitation(TechnicianInvitationEntity invitation)
        {
            if (invitation == null)
            {
                throw new ArgumentNullException(nameof(invitation));
            }

            using var connection = _connectionFactory.Build();
            const string storedProcedure = "Technician.Invitation_Create @TechnicianId, @InvitationToken, @InvitationDate";
            var technicianInvitations = await connection.QueryAsync<TechnicianInvitationEntity>(storedProcedure, new
            { 
                invitation.TechnicianId,
                invitation.InvitationToken,
                invitation.InvitationDate
            });
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
            const string storedProcedure = "Technician.Invitation_Accept @TechnicianId, @UserId";
            await connection.ExecuteAsync(storedProcedure, new
            {
                TechnicianId = technicianId,
                UserId = userId
            });
        }
    }
}
