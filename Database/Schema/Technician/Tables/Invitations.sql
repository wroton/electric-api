﻿CREATE TABLE Technician.Invitations
(
	TechnicianId INT NOT NULL,
	InvitationToken NCHAR(1024) NOT NULL,
	InvitationDate DATETIME2(0) NOT NULL,
	CONSTRAINT [PK_Technician_Invitations] PRIMARY KEY CLUSTERED (TechnicianId)
);
