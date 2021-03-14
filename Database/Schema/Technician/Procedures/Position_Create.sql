CREATE PROCEDURE Technician.Position_Create
	@Name NVARCHAR(50),
	@BusinessId INT
AS

INSERT INTO Technician.Positions
	([Name], BusinessId)
VALUES
	(@Name, @BusinessId);

SELECT
	*
FROM
	Technician.vPositions
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Technician.Position_Create TO [ElectricApi];
GO
