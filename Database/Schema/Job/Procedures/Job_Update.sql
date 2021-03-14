CREATE PROCEDURE Job.Job_Update
	@Id INT,
	@Title NVARCHAR(200),
	@StartTime DATETIME2(0),
	@EndTime DATETIME2(0),
	@Estimate DECIMAL(9, 2),
	@OpenAssignment BIT,
	@Description NVARCHAR(MAX),
	@ClientId INT,
	@TechnicianId INT
AS

UPDATE
	Job.Jobs
SET
	Title = @Title,
	StartTime = @StartTime,
	EndTime = @EndTime,
	Estimate = @Estimate,
	OpenAssignment = @OpenAssignment,
	[Description] = @Description,
	ClientId = @ClientId,
	TechnicianId = @TechnicianId
WHERE
	Id = @Id;

SELECT
	*
FROM
	Job.vJobs
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Job.Job_Update TO [ElectricApi];
GO
