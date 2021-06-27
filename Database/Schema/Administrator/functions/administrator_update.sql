CREATE OR REPLACE FUNCTION administrator.administrator_update
(
    id INT,
    "name" VARCHAR(200),
    businessid INT,
    userid INT
)
RETURNS SETOF administrator.v_administrators
LANGUAGE SQL AS
$$

UPDATE
    administrator.administrators
SET
    "name" = "name",
    businessid = businessid,
    userid = userid
WHERE
    id = id;

SELECT
    *
FROM
    administrator.v_administrators
WHERE
    id = id;
$$
