CREATE PROCEDURE Technician.Invitation_Create
	@TechnicianId INT,
	@InvitationToken NCHAR(1024),
	@InvitationDate DATETIME2(0)
AS

INSERT INTO Technician.Invitations
	(TechnicianId, InvitationToken, InvitationDate)
VALUES
	(@TechnicianId, @InvitationToken, @InvitationDate);

SELECT
	*
FROM
	Technician.vInvitations
WHERE
	TechnicianId = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Technician.Invitation_Create TO ElectricApi;
GO
