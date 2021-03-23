CREATE VIEW Administrator.vAdministrators
AS

SELECT
	a.Id,
	a.[Name],
	b.Id AS [BusinessId],
	b.[Name] AS [BusinessName],
	a.UserId
FROM
	Administrator.Administrators AS a
	INNER JOIN Business.Businesses AS b ON b.Id = a.BusinessId;
GO

GRANT SELECT ON Administrator.vAdministrators TO ElectricApi;
GO
