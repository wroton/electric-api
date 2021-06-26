CREATE OR REPLACE FUNCTION technician.position_create
(
	"name" VARCHAR(50),
	businessid INT
)
RETURNS SETOF technician.v_positions
LANGUAGE SQL AS
$$

INSERT INTO technician.positions
	("name", businessid)
VALUES
	("name", businessid);

SELECT
	*
FROM
	technician.v_positions
WHERE
	id = LASTVAL();
$$

