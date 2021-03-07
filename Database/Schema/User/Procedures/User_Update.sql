CREATE PROCEDURE [User].User_Update
	@Id INT,
	@Password NVARCHAR(50)
AS

UPDATE
	[User].Users
SET
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

GRANT EXECUTE ON [User].User_Update TO [Service];
GO
