CREATE PROCEDURE Administrator.Administrator_Create
	@Name NVARCHAR(200),
	@BusinessId INT
AS

INSERT INTO Administrator.Administrators
	([Name], BusinessId)
VALUES
	(@Name, @BusinessId);

SELECT
	*
FROM
	Administrator.vAdministrators
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON Administrator.Administrator_Create TO ElectricApi;
GO
