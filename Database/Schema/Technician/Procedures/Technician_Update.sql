CREATE PROCEDURE Technician.Technician_Update
	@Id INT,
	@Name NVARCHAR(100),
	@PositionId INT
AS

UPDATE
	Technician.Technicians
SET
	[Name] = @Name,
	PositionId = @PositionId
WHERE
	Id = @Id;

SELECT
	*
FROM
	Technician.vTechnicians
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Technician.Technician_Update TO ElectricApi;
GO
