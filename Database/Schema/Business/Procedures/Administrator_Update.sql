CREATE PROCEDURE Business.Administrator_Update
	@Id INT,
	@Name NVARCHAR(200),
	@BusinessId INT,
	@UserId INT
AS

UPDATE
	Business.Administrators
SET
	[Name] = @Name,
	BusinessId = @BusinessId,
	UserId = @UserId
WHERE
	Id = @Id;

SELECT
	*
FROM
	Business.vAdministrators
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Business.Administrator_Update TO ElectricApi;
GO
