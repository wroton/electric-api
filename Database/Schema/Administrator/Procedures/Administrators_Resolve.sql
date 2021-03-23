CREATE PROCEDURE Administrator.Administrators_Resolve
	@Ids VARCHAR(MAX)
AS

SELECT
	a.*
FROM
	STRING_SPLIT(@Ids, ',') AS ids
	INNER JOIN Administrator.vAdministrators AS a ON a.Id = ids.[value];
GO

GRANT EXECUTE ON Administrator.Administrators_Resolve TO ElectricApi;
GO
