CREATE PROCEDURE Technician.Technicians_Search
	@UserId INT,
	@Name NVARCHAR(100)
AS

SELECT
	t.Id
FROM
	Technician.Technicians AS t
	INNER JOIN [User].Users AS u ON u.Id = @UserId
WHERE
	(@Name IS NULL OR t.[Name] LIKE '%' + @Name + '%') OR -- If the name is provided, filter by it.
	(u.BusinessId IS NULL OR u.BusinessId = t.BusinessId); -- User must be an admin or in the same business.
GO

GRANT EXECUTE ON Technician.Technicians_Search TO ElectricApi;
GO
