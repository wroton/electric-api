CREATE OR REPLACE FUNCTION technician.technicians_resolve
(
    ids INT[]
)
RETURNS SETOF technician.v_technicians
LANGUAGE SQL AS
$$

SELECT
    t.*
FROM
    technician.v_technicians AS t
    INNER JOIN UNNEST(ids) AS i ON i = t.id;
$$;
