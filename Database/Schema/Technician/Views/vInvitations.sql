CREATE VIEW Technician.vInvitations
AS

SELECT
	TechnicianId,
	InvitationToken,
	InvitationDate
FROM
	Technician.Invitations;
GO

GRANT SELECT ON Technician.vInvitations TO ElectricApi;
GO
