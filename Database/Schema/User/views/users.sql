CREATE VIEW "user".v_users
AS

SELECT
    id,
    emailaddress,
    "password",
    businessid
FROM
    "user".users;
