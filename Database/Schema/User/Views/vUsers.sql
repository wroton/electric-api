CREATE VIEW [User].vUsers
AS

SELECT
	u.Id,
	u.Username,
	u.[Password],
	b.Id AS [BusinessId],
	b.[Name] AS BusinessName
FROM
	[User].Users AS u
	LEFT JOIN Business.Businesses AS b ON b.Id = u.BusinessId;
GO

GRANT SELECT ON [User].vUsers TO [ElectricApi];
GO
