CREATE PROCEDURE Technician.Technicians_Resolve
	@Ids VARCHAR(MAX)
AS

SELECT
	t.*
FROM
	STRING_SPLIT(@Ids, ',') AS ids
	INNER JOIN Technician.vTechnicians AS t ON t.Id = ids.[value];
GO

GRANT EXECUTE ON Technician.Technicians_Resolve TO [ElectricApi];
GO
