﻿CREATE TABLE Administrator.Administrators
(
	Id INT NOT NULL IDENTITY (-2147483648, 1),
	[Name] NVARCHAR(200) NOT NULL,
	BusinessId INT NOT NULL,
	UserId INT NULL,
	CONSTRAINT [PK_Administrator_Administrators] PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT [FK_Administrator_Administrators_Business_Businesses] FOREIGN KEY (BusinessId) REFERENCES Business.Businesses (Id),
	CONSTRAINT [FK_Administrator_Administrators_User_Users] FOREIGN KEY (UserId) REFERENCES [User].Users (Id),
	CONSTRAINT [CK_Administrator_Administrators_Name] CHECK (LTRIM(RTRIM([Name])) != ''),
	INDEX [IX_Administrator_Administrators_UserId] NONCLUSTERED (UserId)
);
