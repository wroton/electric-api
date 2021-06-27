CREATE VIEW administrator.v_invitations
AS

SELECT
    administratorid,
    invitationtoken,
    invitationdate
FROM
    administrator.invitations;
