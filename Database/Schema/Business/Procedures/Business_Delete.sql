CREATE PROCEDURE Business.Business_Delete
	@Id INT
AS

DELETE FROM
	Business.Businesses
WHERE
	Id = @Id;
GO

GRANT EXECUTE ON Business.Business_Delete TO [Service];
GO
