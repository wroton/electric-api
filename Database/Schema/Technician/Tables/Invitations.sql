CREATE TABLE technician.invitations
(
    technicianid INT PRIMARY KEY REFERENCES technician.technicians (id),
    invitationtoken CHAR(1024) NOT NULL,
    invitationdate TIMESTAMP(0) NOT NULL
);
