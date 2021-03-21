CREATE PROCEDURE [User].User_Create
	@EmailAddress NVARCHAR(320),
	@Password NCHAR(64)
AS

INSERT INTO [User].Users
	(EmailAddress, [Password])
VALUES
	(@EmailAddress, @Password);

SELECT
	*
FROM
	[User].vUsers
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON [User].User_Create TO ElectricApi;
GO
