CREATE OR REPLACE FUNCTION "user".user_create
(
    emailaddress VARCHAR(320),
    "password" NCHAR(64),
    businessid INT
)
RETURNS SETOF "user".v_users
LANGUAGE SQL AS
$$

INSERT INTO "user".users
	(emailaddress, "password", businessid)
VALUES
    (emailaddress, "password", businessid);

SELECT
	*
FROM
	"user".v_users
WHERE
	id = LASTVAL();
$$
