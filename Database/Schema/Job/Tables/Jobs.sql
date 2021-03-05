CREATE TABLE Job.Jobs
(
	Id INT NOT NULL IDENTITY (-2147483648, 1),
	Title NVARCHAR(200) NOT NULL,
	StartTime DATETIME2(0) NOT NULL,
	EndTime DATETIME2(0) NOT NULL,
	Estimate DECIMAL(11, 2) NULL,
	OpenAssignment BIT NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	BusinessId INT NOT NULL,
	ClientId INT NOT NULL,
	TechnicianId INT NULL
	CONSTRAINT [PK_Job_Jobs] PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT [FK_Job_Jobs_Client_Clients] FOREIGN KEY (ClientId) REFERENCES Client.Clients (Id),
	CONSTRAINT [FK_Job_Jobs_Technician_Technicians] FOREIGN KEY (TechnicianId) REFERENCES Technician.Technicians (Id),
	CONSTRAINT [CK_Job_Jobs_TitlePopulated] CHECK (LTRIM(RTRIM(Title)) != ''),
	CONSTRAINT [CK_Job_Jobs_EndTimeAfterStartTime] CHECK (EndTime > StartTime),
	INDEX [IX_Job_Jobs_BusinessId] NONCLUSTERED (BusinessId),
	INDEX [IX_Job_Jobs_ClientId] NONCLUSTERED (ClientId),
	INDEX [IX_Job_Jobs_TechnicianId] NONCLUSTERED (TechnicianId)
);
