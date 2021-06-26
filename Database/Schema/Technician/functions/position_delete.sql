CREATE OR REPLACE FUNCTION technician.position_delete
(
    id INT
)
RETURNS VOID
LANGUAGE SQL AS
$$

-- Default position of technicians with the position.
UPDATE
	technician.technicians
SET
	positionid = 0
WHERE
	positionid = id;

-- Delete the position.
DELETE FROM
	technician.positions
WHERE
	id = id;
$$
