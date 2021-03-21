CREATE PROCEDURE Business.Administrator_Delete
	@Id INT
AS

DELETE FROM
	Business.Administrators
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Business.Administrator_Delete TO ElectricApi;
GO
