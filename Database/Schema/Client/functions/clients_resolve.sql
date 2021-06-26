CREATE OR REPLACE FUNCTION client.clients_resolve
(
    ids INT[]
)
RETURNS SETOF client.v_clients
LANGUAGE SQL AS
$$

SELECT
    c.*
FROM
    client.v_clients AS c
    INNER JOIN UNNEST(ids) AS i ON i = c.id;
$$;
