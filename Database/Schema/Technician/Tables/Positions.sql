CREATE TABLE technician.positions
(
    id SERIAL PRIMARY KEY,
    "name" VARCHAR(50) NOT NULL CHECK (TRIM("name") != ''),
    businessid INT NOT NULL REFERENCES business.businesses (id) 
);

ALTER SEQUENCE technician.positions_id_seq MINVALUE -2147483648;