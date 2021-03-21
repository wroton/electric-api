CREATE PROCEDURE Business.Business_Update
	@Id INT,
	@Name NVARCHAR(200),
	@AddressLine1 VARCHAR(150),
	@AddressLine2 VARCHAR(150),
	@City VARCHAR(100),
	@State CHAR(2),
	@ZipCode VARCHAR(10)
AS

UPDATE
	Business.Businesses
SET
	[Name] = @Name,
	AddressLine1 = @AddressLine1,
	AddressLine2 = @AddressLine2,
	City = @City,
	[State] = @State,
	ZipCode = @ZipCode
WHERE
	Id = @Id;

SELECT
	*
FROM
	Business.vBusinesses
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Business.Business_Update TO ElectricApi;
GO
