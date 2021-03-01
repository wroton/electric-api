CREATE PROCEDURE Technician.Technician_Create
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@UserId INT
AS

INSERT INTO Technician.Technicians
	(FirstName, LastName, UserId)
VALUES
	(@FirstName, @LastName, @UserId)

SELECT
	*
FROM
	Technician.vTechnicians
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Technician.Technician_Create TO [Service];
GO
