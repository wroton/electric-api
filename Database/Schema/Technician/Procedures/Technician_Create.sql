CREATE PROCEDURE Technician.Technician_Create
	@Name NVARCHAR(100),
	@UserId INT
AS

INSERT INTO Technician.Technicians
	([Name], UserId)
VALUES
	(@Name, @UserId);

SELECT
	*
FROM
	Technician.vTechnicians
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Technician.Technician_Create TO [ElectricApi];
GO
