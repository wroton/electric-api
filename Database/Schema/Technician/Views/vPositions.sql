CREATE VIEW Technician.vPositions
AS

SELECT
	Id,
	[Name],
	BusinessId
FROM
	Technician.Positions;
GO

GRANT SELECT ON Technician.vPositions TO [Service];
GO
