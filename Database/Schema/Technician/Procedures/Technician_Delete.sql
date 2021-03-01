CREATE PROCEDURE Technician.Technician_Delete
	@Id INT
AS

DELETE FROM
	Technician.Technicians
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Technician.Technician_Delete TO [Service];
GO
