CREATE PROCEDURE Job.Job_Create
	@Title NVARCHAR(200),
	@StartTime DATETIME2(0),
	@EndTime DATETIME2(0),
	@Estimate DECIMAL(9, 2),
	@OpenAssignment BIT,
	@Description NVARCHAR(MAX),
	@BusinessId INT,
	@ClientId INT,
	@TechnicianId INT
AS

INSERT INTO Job.Jobs
	(Title, StartTime, EndTime, Estimate,
	 OpenAssignment, [Description], BusinessId,
	 ClientId, TechnicianId)
VALUES
	(@Title, @StartTime, @EndTime, @Estimate,
	 @OpenAssignment, @Description, @BusinessId,
	 @ClientId, @TechnicianId);

SELECT
	*
FROM
	Job.vJobs
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Job.Job_Create TO ElectricApi;
GO
