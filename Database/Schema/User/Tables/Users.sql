CREATE TABLE [User].Users
(
	Id INT NOT NULL IDENTITY (-2147483648, 1),
	EmailAddress NVARCHAR(320) NOT NULL,
	[Password] NCHAR(64) NOT NULL,
	BusinessId INT NULL,
	CONSTRAINT [PK_User_Users] PRIMARY KEY CLUSTERED (Id),
	INDEX [IX_User_Users_EmailAddress] NONCLUSTERED (EmailAddress),
	CONSTRAINT [FK_User_Users_Business_Businesses_BusinessId] FOREIGN KEY (BusinessId) REFERENCES Business.Businesses (Id)
);
