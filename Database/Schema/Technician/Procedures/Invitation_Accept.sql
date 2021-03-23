CREATE PROCEDURE Technician.Invitation_Accept
	@TechnicianId INT,
	@UserId INT
AS

BEGIN TRANSACTION;
BEGIN TRY

-- Bind the technician to the user.
UPDATE
	Technician.Technicians
SET
	UserId = @UserId
WHERE
	Id = @TechnicianId;

-- Delete the invitation.
DELETE FROM
	Technician.Invitations
WHERE
	TechnicianId = @TechnicianId;

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

GRANT EXECUTE ON Technician.Invitation_Accept TO ElectricApi;
GO
