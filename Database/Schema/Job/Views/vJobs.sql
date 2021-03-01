CREATE VIEW Job.vJobs
AS
	SELECT
		Id,
		Title,
		StartTime,
		EndTime
	FROM
		Job.Jobs;
GO

GRANT SELECT ON Job.vJobs TO [Service];
GO
