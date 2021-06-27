CREATE OR REPLACE FUNCTION technician.invitation_create
(
	technicianid INT,
	invitationtoken CHAR(1024),
	invitationdate TIMESTAMP(0)
)
RETURNS SETOF technician.v_invitations
LANGUAGE SQL AS
$$

INSERT INTO technician.invitations
	(technicianid, invitationtoken, invitationdate)
VALUES
	(technicianid, invitationtoken, invitationdate);

SELECT
	*
FROM
	technician.v_invitations
WHERE
	technicianid = LASTVAL();
$$
