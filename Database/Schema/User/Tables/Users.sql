CREATE TABLE [User].Users
(
	Id INT NOT NULL IDENTITY (-2147483648, 1),
	Username NVARCHAR(50) NOT NULL,
	[Password] NCHAR(64) NOT NULL,
	BusinessId INT NULL,
	CONSTRAINT [PK_User_Users] PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT [FK_User_Users_Business_Businesses] FOREIGN KEY (BusinessId) REFERENCES Business.Businesses (Id),
	INDEX [IX_User_Users_Username] NONCLUSTERED (Username),
	INDEX [IX_User_Users_BusinessId] NONCLUSTERED (BusinessId)
);
