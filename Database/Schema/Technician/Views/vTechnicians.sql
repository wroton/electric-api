CREATE VIEW Technician.vTechnicians
AS

SELECT
	t.Id,
	t.[Name],
	t.BusinessId,
	b.[Name] AS [BusinessName],
	UserId
FROM
	Technician.Technicians AS t
	INNER JOIN Business.Businesses AS b ON b.Id = t.BusinessId;
GO

GRANT SELECT ON Technician.vTechnicians TO ElectricApi;
GO
