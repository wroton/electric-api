CREATE VIEW Business.vBusinesses
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
	Business.Businesses;
GO

GRANT SELECT ON Business.vBusinesses TO ElectricApi;
GO
