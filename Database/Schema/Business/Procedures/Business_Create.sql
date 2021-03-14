CREATE PROCEDURE Business.Business_Create
	@Name NVARCHAR(200),
	@AddressLine1 VARCHAR(150),
	@AddressLine2 VARCHAR(150),
	@City VARCHAR(100),
	@State CHAR(2),
	@ZipCode VARCHAR(10)
AS

INSERT INTO Business.Businesses
	([Name], AddressLine1, AddressLine2,
	 City, [State], ZipCode)
VALUES
	(@Name, @AddressLine1, @AddressLine2,
	 @City, @State, @ZipCode);

SELECT
	*
FROM
	Business.vBusinesses
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Business.Business_Create TO [ElectricApi];
GO
