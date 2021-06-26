CREATE OR REPLACE FUNCTION "user".users_resolve
(
    ids INT[]
)
RETURNS SETOF "user".v_users
LANGUAGE SQL AS
$$

SELECT
    u.*
FROM
    "user".v_users AS u
    INNER JOIN UNNEST(ids) AS i ON i = u.id;
$$
