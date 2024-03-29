﻿CREATE VIEW [User].vUsers
AS

SELECT
	u.Id,
	u.EmailAddress,
	u.[Password],
	u.BusinessId
FROM
	[User].Users AS u;
GO

GRANT SELECT ON [User].vUsers TO ElectricApi;
GO
