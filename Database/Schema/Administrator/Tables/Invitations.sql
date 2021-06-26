CREATE TABLE administrator.invitations
(
    administratorid INT PRIMARY KEY REFERENCES administrator.administrators (id),
    invitationtoken CHAR(1024) NOT NULL,
    invitationdate TIMESTAMP(0) NOT NULL
);
