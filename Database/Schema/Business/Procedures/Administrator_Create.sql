CREATE PROCEDURE Business.Administrator_Create
	@Name NVARCHAR(200),
	@BusinessId INT,
	@UserId INT
AS

INSERT INTO Business.Administrators
	([Name], BusinessId, UserId)
VALUES
	(@Name, @BusinessId, @UserId);

SELECT
	*
FROM
	Business.vAdministrators
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Business.Administrator_Create TO ElectricApi;
GO
