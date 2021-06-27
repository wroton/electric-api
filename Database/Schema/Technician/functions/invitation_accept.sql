CREATE OR REPLACE FUNCTION technician.invitation_accept
(
	technicianid INT,
	userid INT
)
RETURNS VOID
LANGUAGE SQL AS
$$

-- Bind the technician to the user.
UPDATE
	technician.technicians
SET
	userid = userid
WHERE
	id = technicianId;

-- Delete the invitation.
DELETE FROM
	technician.invitations
WHERE
	technicianId = technicianid;
$$