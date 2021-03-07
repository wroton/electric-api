CREATE PROCEDURE [User].Users_Resolve
	@Ids VARCHAR(MAX)
AS

SELECT
	u.*
FROM
	STRING_SPLIT(@Ids, ',') AS ids
	INNER JOIN [User].vUsers AS u ON u.Id = ids.[value];
GO

GRANT EXECUTE ON [User].Users_Resolve TO [Service];
GO
