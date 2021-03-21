CREATE PROCEDURE [User].User_Delete
	@Id INT
AS

BEGIN TRY;
BEGIN TRANSACTION;

-- Find all technicians with the user and delink them.
UPDATE
	Technician.Technicians
SET
	UserId = NULL
WHERE
	UserId = @Id;

-- Delete the user.
DELETE FROM
	[User].Users
WHERE
	Id = @Id;

COMMIT TRANSACTION;
END TRY
BEGIN CATCH

IF @@TRANCOUNT > 0
BEGIN
	ROLLBACK TRANSACTION;
END

;THROW;

END CATCH
GO

GRANT EXECUTE ON [User].User_Delete TO ElectricApi;
GO
