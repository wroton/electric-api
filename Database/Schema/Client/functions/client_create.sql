CREATE OR REPLACE FUNCTION client.client_create
(
	"name" VARCHAR(200),
	addressline1 VARCHAR(150),
	addressline2 VARCHAR(150),
	city VARCHAR(100),
	"state" CHAR(2),
	zipcode VARCHAR(10)
)
RETURNS SETOF client.v_clients
LANGUAGE SQL AS
$$

INSERT INTO client.clients
	("name", addressline1, addressline2,
	 city, "state", zipcode)
VALUES
	("name", addressline1, addressline2,
	 city, "state", zipcode);

SELECT
	*
FROM
    client.v_clients
WHERE
	id = LASTVAL();
$$
