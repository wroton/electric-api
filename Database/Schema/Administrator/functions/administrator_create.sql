CREATE OR REPLACE FUNCTION administrator.administrator_create
(
    "name" VARCHAR(200),
    businessid INT
)
RETURNS SETOF administrator.v_administrators
LANGUAGE SQL AS
$$

INSERT INTO administrator.administrators
    ("name", businessid)
VALUES
    ("name", businessid);

SELECT
    *
FROM
    administrator.v_administrators
WHERE
    id = LASTVAL();
$$
