CREATE TABLE Technician.Technicians
(
	Id INT NOT NULL IDENTITY (-2147483648, 1),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	UserId INT NULL,
	CONSTRAINT [PK_Technician_Technicians] PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT [RK_Technician_Technicians_User_Users_UserId] FOREIGN KEY (UserId) REFERENCES [User].Users (Id)
);
