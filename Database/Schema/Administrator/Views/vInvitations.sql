CREATE VIEW Administrator.vInvitations
AS

SELECT
	AdministratorId,
	InvitationToken,
	InvitationDate
FROM
	Administrator.Invitations;
GO

GRANT SELECT ON Administrator.vInvitations TO ElectricApi;
GO
