CREATE PROCEDURE Business.Administrators_Resolve
	@Ids VARCHAR(MAX)
AS

SELECT
	a.*
FROM
	STRING_SPLIT(@Ids, ',') AS ids
	INNER JOIN Business.vAdministrators AS a ON a.Id = ids.[value];
GO

GRANT EXECUTE ON Business.Administrators_Resolve TO ElectricApi;
GO
