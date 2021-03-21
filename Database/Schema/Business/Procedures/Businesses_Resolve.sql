CREATE PROCEDURE Business.Businesses_Resolve
	@Ids VARCHAR(MAX)
AS

SELECT
	b.*
FROM
	STRING_SPLIT(@Ids, ',') AS ids
	INNER JOIN Business.vBusinesses AS b ON b.Id = ids.[value];
GO

GRANT EXECUTE ON Business.Businesses_Resolve TO ElectricApi;
GO
