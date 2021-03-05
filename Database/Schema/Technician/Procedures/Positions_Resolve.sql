CREATE PROCEDURE Technician.Positions_Resolve
	@Ids VARCHAR(MAX)
AS

SELECT
	p.*
FROM
	STRING_SPLIT(@Ids, ',') AS ids
	INNER JOIN Technician.vPositions AS p ON p.Id = ids.[value];
GO

GRANT EXECUTE ON Technician.Positions_Resolve TO [Service];
GO
