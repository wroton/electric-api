CREATE TABLE Technician.Technicians
(
	Id INT NOT NULL IDENTITY (-2147483648, 1),
	[Name] NVARCHAR(100) NOT NULL,
	PositionId TINYINT NOT NULL,
	BusinessId INT NOT NULL,
	UserId INT NULL,
	CONSTRAINT [PK_Technician_Technicians] PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT [FK_Technician_Technicians_Technician_Positions] FOREIGN KEY (PositionId) REFERENCES Technician.Positions (Id),
	CONSTRAINT [FK_Technician_Technicians_Business_Businesses] FOREIGN KEY (BusinessId) REFERENCES Business.Businesses (Id),
	CONSTRAINT [FK_Technician_Technicians_User_Users] FOREIGN KEY (UserId) REFERENCES [User].Users (Id),
	INDEX [IX_Technician_Technicians_UserId] NONCLUSTERED (UserId)
);
