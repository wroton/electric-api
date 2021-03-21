CREATE VIEW Job.vJobs
AS

SELECT
	j.Id,
	j.Title,
	j.StartTime,
	j.EndTime,
	j.Estimate,
	j.[Description],
	j.OpenAssignment,
	j.BusinessId,
	b.[Name] AS [BusinessName],
	j.ClientId,
	c.[Name] AS [ClientName],
	j.TechnicianId,
	t.[Name] AS [TechnicianName]
FROM
	Job.Jobs AS j
	INNER JOIN Business.Businesses AS b ON b.Id = j.BusinessId
	INNER JOIN Client.Clients AS c ON c.Id = j.ClientId
	INNER JOIN Technician.Technicians AS t ON t.Id = j.TechnicianId;
GO

GRANT SELECT ON Job.vJobs TO ElectricApi;
GO
