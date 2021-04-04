CREATE PROCEDURE Technician.Positions_List
	@UserId INT
AS

SELECT
	p.*
FROM
	Technician.Positions AS p
	INNER JOIN [User].Users AS u ON u.Id = @UserId
WHERE
	u.BusinessId IS NULL OR u.BusinessId = p.BusinessId;
GO

GRANT EXECUTE ON Technician.Positions_List TO ElectricApi;
GO
