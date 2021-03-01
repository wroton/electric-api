CREATE VIEW Technician.vTechnicians
AS

SELECT
	Id,
	FirstName,
	LastName,
	FullName,
	UserId
FROM
	Technician.Technicians;
GO
