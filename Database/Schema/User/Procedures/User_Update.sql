CREATE PROCEDURE [User].User_Update
	@Id INT,
	@Username NVARCHAR(50),
	@Password NCHAR(64)
AS

UPDATE
	[User].Users
SET
	Username = @Username,
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

GRANT EXECUTE ON [User].User_Update TO [ElectricApi];
GO
