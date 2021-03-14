CREATE VIEW Technician.vTechnicians
AS

SELECT
	t.Id,
	t.[Name],
	UserId
FROM
	Technician.Technicians AS t;
GO

GRANT SELECT ON Technician.vTechnicians TO [ElectricApi];
GO
