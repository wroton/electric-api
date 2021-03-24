CREATE TABLE Administrator.Invitations
(
	AdministratorId INT NOT NULL,
	InvitationToken NCHAR(1024) NOT NULL,
	InvitationDate DATETIME2(0) NOT NULL,
	CONSTRAINT [PK_Administrator_Invitations] PRIMARY KEY CLUSTERED (AdministratorId)
);
