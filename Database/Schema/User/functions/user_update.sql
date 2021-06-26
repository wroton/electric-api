CREATE OR REPLACE FUNCTION "user".user_update
(
    id INT,
    emailaddress VARCHAR(320),
    "password" NCHAR(64),
    businessid INT
)
RETURNS SETOF "user".v_users
LANGUAGE SQL AS
$$

UPDATE
    "user".users
SET
    emailaddress = emailaddress,
    "password" = "password",
    businessid = businessid
WHERE
    id = id;

SELECT
	*
FROM
	"user".v_users
WHERE
	id = id;
$$
