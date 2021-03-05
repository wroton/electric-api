MERGE INTO Technician.Positions AS t
USING
(
	VALUES
		(0, 'No Position')
) AS s (Id, [Name])
ON t.Id = s.Id
WHEN MATCHED THEN
UPDATE
SET
	t.[Name] = s.[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT
	(Id, [Name])
VALUES
	(s.Id, s.[Name])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
