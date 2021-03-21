CREATE TABLE Technician.TechnicianInvitations
(
	TechnicianId INT NOT NULL,
	InvitationToken NCHAR(64) NOT NULL,
	InvitationDate DATETIME2(0) NOT NULL,
	CONSTRAINT [PK_Technician_TechnicianInvitations] PRIMARY KEY CLUSTERED (TechnicianId)
);
