CREATE OR REPLACE FUNCTION technician.technician_create
(
    "name" VARCHAR(100),
    positionid INT,
    businessid INT,
    userid INT
)
RETURNS SETOF technician.v_technicians
LANGUAGE SQL AS
$$

INSERT INTO technician.technicians
	("name", positionid, businessid, userid)
VALUES
	("name", positionid, businessid, userid);

SELECT
	*
FROM
	technician.v_technicians
WHERE
	id = LASTVAL();
$$
