CREATE OR REPLACE FUNCTION business.business_create
(
    "name" VARCHAR(200),
    addressline1 VARCHAR(150),
    addressline2 VARCHAR(150),
    city VARCHAR(100),
    "state" CHAR(2),
    zipcode VARCHAR(10)
)
RETURNS SETOF business.v_businesses
LANGUAGE SQL AS
$$

INSERT INTO business.businesses
    ("name", addressline1, addressline2,
     city, "state", zipcode)
VALUES
    ("name", addressline1, addressline2,
     city, "state", zipcode);

SELECT
    *
FROM
    business.v_businesses
WHERE
    id = LASTVAL();
$$
