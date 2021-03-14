CREATE PROCEDURE Technician.Position_Update
	@Id INT,
	@Name NVARCHAR(50)
AS

UPDATE
	Technician.Positions
SET
	[Name] = @Name
WHERE
	Id = @Id;

SELECT
	*
FROM
	Technician.vPositions
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Technician.Position_Update TO [ElectricApi];
GO
