CREATE OR REPLACE VIEW technician.v_invitations
AS

SELECT
    technicianid,
    invitationtoken,
    invitationdate
FROM
    technician.invitations;
