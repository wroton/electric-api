CREATE PROCEDURE Administrator.Administrators_Search
	@UserId INT,
	@Name NVARCHAR(100)
AS

SELECT
	a.Id
FROM
	Administrator.Administrators AS a
	INNER JOIN [User].Users AS u ON u.Id = @UserId
WHERE
	(u.BusinessId IS NULL OR u.BusinessId = a.BusinessId) AND
	(@Name IS NULL OR a.[Name] LIKE '%' + @Name + '%');
GO

GRANT EXECUTE ON Administrator.Administrators_Search TO ElectricApi;
GO
