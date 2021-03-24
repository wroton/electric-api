CREATE PROCEDURE Administrator.Invitation_Create
	@AdministratorId INT,
	@InvitationToken NCHAR(1024),
	@InvitationDate DATETIME2(0)
AS

INSERT INTO Administrator.Invitations
	(AdministratorId, InvitationToken, InvitationDate)
VALUES
	(@AdministratorId, @InvitationToken, @InvitationDate);

SELECT
	*
FROM
	Administrator.vInvitations
WHERE
	AdministratorId = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Administrator.Invitation_Create TO ElectricApi;
GO
