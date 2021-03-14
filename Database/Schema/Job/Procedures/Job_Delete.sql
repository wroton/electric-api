CREATE PROCEDURE Job.Job_Delete
	@Id INT
AS

DELETE FROM
	Job.Jobs
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Job.Job_Delete TO [ElectricApi];
GO
