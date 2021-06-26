CREATE OR REPLACE FUNCTION technician.position_update
(
    id INT,
	"name" VARCHAR(50)
)
RETURNS SETOF technician.v_positions
LANGUAGE SQL AS
$$

UPDATE
    technician.positions
SET
    "name" = "name"
WHERE
    id = id;

SELECT
	*
FROM
	technician.v_positions
WHERE
	id = LASTVAL();
$$
