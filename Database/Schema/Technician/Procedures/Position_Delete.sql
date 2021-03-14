CREATE PROCEDURE Technician.Position_Delete
	@Id INT
AS

BEGIN TRY;
BEGIN TRANSACTION;

-- Update all technicians with the position to 'No Position'.
UPDATE
	Technician.Technicians
SET
	PositionId = 0
WHERE
	PositionId = @Id;

-- Delete the position.
DELETE FROM
	Technician.Positions
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

GRANT EXECUTE ON Technician.Position_Delete TO [ElectricApi];
GO
