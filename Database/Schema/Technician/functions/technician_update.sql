CREATE OR REPLACE FUNCTION technician.technician_update
(
    id INT,
    "name" VARCHAR(100),
    positionid INT,
    businessid INT,
    userid INT
)
RETURNS SETOF technician.v_technicians
LANGUAGE SQL AS
$$

UPDATE
    technician.technicians
SET
    "name" = "name",
    positionid = positionid;

SELECT
	*
FROM
	technician.v_technicians
WHERE
	id = id;
$$
