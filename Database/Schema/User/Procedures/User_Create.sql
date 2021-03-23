CREATE PROCEDURE [User].User_Create
	@EmailAddress NVARCHAR(320),
	@Password NCHAR(64),
	@BusinessId INT
AS

INSERT INTO [User].Users
	(EmailAddress, [Password], BusinessId)
VALUES
	(@EmailAddress, @Password, @BusinessId);

SELECT
	*
FROM
	[User].vUsers
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON [User].User_Create TO ElectricApi;
GO
