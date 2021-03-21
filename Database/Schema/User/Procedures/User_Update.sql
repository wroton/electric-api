CREATE PROCEDURE [User].User_Update
	@Id INT,
	@EmailAddress NVARCHAR(320),
	@Password NCHAR(64)
AS

UPDATE
	[User].Users
SET
	EmailAddress = @EmailAddress,
	[Password] = @Password
WHERE
	Id = @Id;

SELECT
	*
FROM
	[User].vUsers
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON [User].User_Update TO ElectricApi;
GO
