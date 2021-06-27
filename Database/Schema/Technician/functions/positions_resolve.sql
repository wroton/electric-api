CREATE OR REPLACE FUNCTION technician.positions_resolve
(
    ids INT[]
)
RETURNS SETOF technician.v_positions
LANGUAGE SQL AS
$$

SELECT
	p.*
FROM
	technician.v_positions AS p
    INNER JOIN UNNEST(ids) AS i ON i = p.id;
$$
