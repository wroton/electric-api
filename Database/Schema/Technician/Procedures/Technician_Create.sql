CREATE PROCEDURE Technician.Technician_Create
	@Name NVARCHAR(100),
	@PositionId INT,
	@BusinessId INT,
	@UserId INT
AS

INSERT INTO Technician.Technicians
	([Name], PositionId, BusinessId, UserId)
VALUES
	(@Name, @PositionId, @BusinessId, @UserId);

SELECT
	*
FROM
	Technician.vTechnicians
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Technician.Technician_Create TO ElectricApi;
GO
