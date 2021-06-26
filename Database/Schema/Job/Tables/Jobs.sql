CREATE TABLE job.jobs
(
    id SERIAL PRIMARY KEY,
    title VARCHAR(200) NOT NULL CHECK (TRIM(title) != ''),
    starttime TIMESTAMP(0) NOT NULL,
    endtime TIMESTAMP(0) NOT NULL CHECK (endtime > starttime),
    estimate MONEY NULL,
    openassignment BOOLEAN NOT NULL,
    "description" TEXT NULL,
    businessid INT NOT NULL REFERENCES business.businesses (id),
    clientid INT NOT NULL REFERENCES client.clients (id),
    technicianid INT NULL REFERENCES technician.technicians (id) 
);

ALTER SEQUENCE job.jobs_id_seq MINVALUE -2147483648;