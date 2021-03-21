CREATE PROCEDURE Client.Clients_Resolve
	@Ids VARCHAR(MAX)
AS

SELECT
	c.*
FROM
	STRING_SPLIT(@Ids, ',') AS ids
	INNER JOIN Client.vClients AS c ON c.Id = ids.[value];
GO

GRANT EXECUTE ON Client.Clients_Resolve TO ElectricApi;
GO
