CREATE PROCEDURE Administrator.Administrator_Create
	@Name NVARCHAR(200),
	@BusinessId INT,
	@UserId INT
AS

INSERT INTO Administrator.Administrators
	([Name], BusinessId, UserId)
VALUES
	(@Name, @BusinessId, @UserId);

SELECT
	*
FROM
	Administrator.vAdministrators
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Administrator.Administrator_Create TO ElectricApi;
GO
