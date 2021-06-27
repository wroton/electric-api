CREATE TABLE technician.technicians
(
    id SERIAL PRIMARY KEY,
    "name" VARCHAR(100) NOT NULL CHECK (TRIM("name") != ''),
    positionid INT NOT NULL REFERENCES technician.positions (id),
    businessid INT NOT NULL REFERENCES business.businesses (id),
    userid INT NULL REFERENCES "user".users (id)
);

CREATE UNIQUE INDEX ix_technician_technicians_userid ON technician.technicians (userid);

ALTER SEQUENCE technician.technicians_id_seq MINVALUE -2147483648;