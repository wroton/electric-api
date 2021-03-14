CREATE PROCEDURE [User].User_Create
	@Username NVARCHAR(100),
	@Password NCHAR(64),
	@BusinessId INT
AS

INSERT INTO [User].Users
	(Username, [Password], BusinessId)
VALUES
	(@Username, @Password, @BusinessId);

SELECT
	*
FROM
	[User].vUsers
WHERE
	Id = SCOPE_IDENTITY();
GO

GRANT EXECUTE ON [User].User_Create TO [ElectricApi];
GO
