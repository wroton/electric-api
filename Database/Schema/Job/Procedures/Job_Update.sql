CREATE PROCEDURE Job.Job_Update
	@Id INT,
	@Title NVARCHAR(200),
	@StartTime DATETIME2(0),
	@EndTime DATETIME2(0)
AS

UPDATE
	Job.Jobs
SET
	Title = @Title,
	StartTime = @StartTime,
	EndTime = @EndTime
WHERE
	Id = @Id;

SELECT
	*
FROM
	Job.vJobs
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Job.Job_Update TO [Service];
GO
