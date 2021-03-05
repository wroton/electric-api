CREATE VIEW Client.vClients
AS

SELECT
	Id,
	[Name],
	AddressLine1,
	AddressLine2,
	City,
	[State],
	ZipCode
FROM
	Client.Clients AS c;
GO

GRANT SELECT ON Client.vClients TO [Service];
GO
