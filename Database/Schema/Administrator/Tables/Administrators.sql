CREATE TABLE administrator.administrators
(
    id SERIAL PRIMARY KEY,
    "name" VARCHAR(200) NOT NULL CHECK (TRIM("name") != ''),
    businessid INT NOT NULL REFERENCES business.businesses (id),
    userid INT NULL REFERENCES "user".users (id) 
);

ALTER SEQUENCE administrator.administrators_id_seq MINVALUE -2147483648;