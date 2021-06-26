CREATE OR REPLACE FUNCTION administrator.administrators_resolve
(
    ids INT[]
)
RETURNS SETOF administrator.v_administrators
LANGUAGE SQL AS
$$

SELECT
    a.*
FROM
    administrator.v_administrators AS a
    INNER JOIN UNNEST(ids) AS i ON i = a.id;
$$;
