CREATE PROCEDURE Job.Job_Create
	@Title NVARCHAR(200),
	@StartTime DATETIME2(0),
	@EndTime DATETIME2(0)
AS

INSERT INTO Job.Jobs
	(Title, StartTime, EndTime)
VALUES
	(@Title, @StartTime, @EndTime);

SELECT
	*
FROM
	Job.vJobs
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Job.Job_Create TO [Service];
GO
