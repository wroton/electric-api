CREATE PROCEDURE Administrator.Administrator_Update
	@Id INT,
	@Name NVARCHAR(200),
	@BusinessId INT,
	@UserId INT
AS

UPDATE
	Administrator.Administrators
SET
	[Name] = @Name,
	BusinessId = @BusinessId,
	UserId = @UserId
WHERE
	Id = @Id;

SELECT
	*
FROM
	Administrator.vAdministrators
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Administrator.Administrator_Update TO ElectricApi;
GO
