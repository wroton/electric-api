CREATE TABLE Technician.Positions
(
	Id INT NOT NULL IDENTITY (0, 1),
	[Name] NVARCHAR(50) NOT NULL,
	BusinessId INT NOT NULL,
	CONSTRAINT [PK_Technician_Positions] PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT [FK_Technician_Positions_Business_Businesses] FOREIGN KEY (BusinessId) REFERENCES Business.Businesses (Id),
	CONSTRAINT [CK_Technician_Technicians_NamePopulated] CHECK (LTRIM(RTRIM([Name])) != '')
);
