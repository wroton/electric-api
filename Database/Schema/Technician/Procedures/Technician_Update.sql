CREATE PROCEDURE Technician.Technician_Update
	@Id INT,
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@UserId INT
AS

UPDATE
	Technician.Technicians
SET
	FirstName = @FirstName,
	LastName = @LastName,
	UserId = @UserId
WHERE
	Id = @Id;

SELECT
	*
FROM
	Technician.vTechnicians
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Technician.Technician_Update TO [Service];
GO
