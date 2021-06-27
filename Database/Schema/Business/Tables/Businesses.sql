CREATE TABLE business.businesses
(
    id SERIAL PRIMARY KEY,
    "name" VARCHAR(200) NOT NULL CHECK (TRIM("name") != ''),
    addressline1 VARCHAR(150) NOT NULL CHECK (TRIM(addressline1) != ''),
    addressline2 VARCHAR(150) NULL,
    city VARCHAR(100) NOT NULL CHECK (TRIM(city) != ''),
    "state" CHAR(2) NOT NULL CHECK (TRIM("state") != ''),
    zipcode VARCHAR(10) NOT NULL CHECK (TRIM(zipcode) != '')
);

ALTER SEQUENCE business.businesses_id_seq MINVALUE -2147483648;