CREATE OR REPLACE FUNCTION client.client_update
(
    id INT,
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

UPDATE
    client.clients
SET
    "name" = "name",
    addressline1 = addressline1,
    addressline2 = addressline2,
    city = city,
    "state" = "state",
    zipcode = zipcode
WHERE
    id = id;

SELECT
    *
FROM
    client.clients
WHERE
    id = id;
$$
