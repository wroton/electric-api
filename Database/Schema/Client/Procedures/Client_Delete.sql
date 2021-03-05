CREATE PROCEDURE Client.Client_Delete
	@Id INT
AS

DELETE FROM
	Client.Clients
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Client.Client_Delete TO [Service];
GO
