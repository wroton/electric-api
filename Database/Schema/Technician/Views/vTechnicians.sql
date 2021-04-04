CREATE VIEW Technician.vTechnicians
AS

SELECT
	t.Id,
	t.[Name],
	t.PositionId,
	p.[Name] AS PositionName,
	t.BusinessId,
	b.[Name] AS [BusinessName],
	UserId
FROM
	Technician.Technicians AS t
	INNER JOIN Technician.Positions AS p ON p.Id = t.PositionId
	INNER JOIN Business.Businesses AS b ON b.Id = t.BusinessId;
GO

GRANT SELECT ON Technician.vTechnicians TO ElectricApi;
GO
