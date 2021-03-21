CREATE VIEW Business.vAdministrators
AS

SELECT
	a.Id,
	a.[Name],
	b.Id AS [BusinessId],
	b.[Name] AS [BusinessName],
	a.UserId
FROM
	Business.Administrators AS a
	INNER JOIN Business.Businesses AS b ON b.Id = a.BusinessId;
GO

GRANT SELECT ON Business.vAdministrators TO ElectricApi;
GO
