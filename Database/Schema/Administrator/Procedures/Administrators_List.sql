CREATE PROCEDURE Administrator.Administrators_List
	@UserId INT
AS

SELECT
	a.Id
FROM
	Administrator.Administrators AS a
	INNER JOIN [User].Users AS u ON u.Id = @UserId
WHERE
	u.BusinessId IS NULL OR u.BusinessId = a.BusinessId;
GO

GRANT EXECUTE ON Administrator.Administrators_List TO ElectricApi;
GO
