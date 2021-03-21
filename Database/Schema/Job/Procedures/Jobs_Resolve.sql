CREATE PROCEDURE Job.Jobs_Resolve
	@Ids VARCHAR(MAX)
AS

SELECT
	j.*
FROM
	STRING_SPLIT(@Ids, ',') AS ids
	INNER JOIN Job.vJobs AS j ON j.Id = ids.[value];
GO

GRANT EXECUTE ON Job.Jobs_Resolve TO ElectricApi;
GO
