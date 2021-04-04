CREATE PROCEDURE Technician.Technicians_Resolve
	@UserId INT,
	@Ids VARCHAR(MAX)
AS

SELECT
	t.*
FROM
	STRING_SPLIT(@Ids, ',') AS ids
	INNER JOIN Technician.vTechnicians AS t ON t.Id = ids.[value]
	INNER JOIN [User].Users AS u ON u.Id = @UserId
WHERE
	u.BusinessId IS NULL OR u.BusinessId = t.BusinessId;
GO

GRANT EXECUTE ON Technician.Technicians_Resolve TO ElectricApi;
GO
