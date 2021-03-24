CREATE PROCEDURE Administrator.Invitation_Accept
	@AdministratorId INT,
	@UserId INT
AS

BEGIN TRANSACTION;
BEGIN TRY

-- Bind the administrator to the user.
UPDATE
	Administrator.Administrators
SET
	UserId = @UserId
WHERE
	Id = @AdministratorId;

-- Delete the invitation.
DELETE FROM
	Administrator.Invitations
WHERE
	AdministratorId = @AdministratorId;

-- Commit the transaction.
COMMIT TRANSACTION;
END TRY
BEGIN CATCH

-- Rollback if needed.
IF @@TRANCOUNT > 0
BEGIN
	ROLLBACK TRANSACTION;
END

;THROW;

END CATCH
GO

GRANT EXECUTE ON Administrator.Invitation_Accept TO ElectricApi;
GO
