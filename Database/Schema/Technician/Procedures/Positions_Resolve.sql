CREATE PROCEDURE Technician.Positions_Resolve
	@UserId INT,
	@Ids VARCHAR(MAX)
AS

SELECT
	p.*
FROM
	STRING_SPLIT(@Ids, ',') AS ids
	INNER JOIN Technician.vPositions AS p ON p.Id = ids.[value]
	INNER JOIN [User].Users AS u ON u.Id = @UserId
WHERE
	u.BusinessId IS NULL OR u.BusinessId = p.BusinessId;
GO

GRANT EXECUTE ON Technician.Positions_Resolve TO ElectricApi;
GO
