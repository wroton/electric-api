CREATE VIEW administrator.v_administrators
AS

SELECT
    id,
    "name",
    businessid,
    userid
FROM
    administrator.administrators;
