CREATE TABLE "user".users
(
    id SERIAL PRIMARY KEY,
    emailaddress VARCHAR(320) NOT NULL CHECK (TRIM(emailaddress) != ''),
    "password" CHAR(64) NOT NULL,
    businessid INT NOT NULL REFERENCES business.businesses (id)
);

CREATE UNIQUE INDEX ix_user_users_emailaddress ON "user".users (emailaddress);

ALTER SEQUENCE "user".users_id_seq MINVALUE -2147483648;