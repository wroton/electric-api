CREATE PROCEDURE Administrator.Administrator_Delete
	@Id INT
AS

DELETE FROM
	Administrator.Administrators
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Administrator.Administrator_Delete TO ElectricApi;
GO
