CREATE OR REPLACE FUNCTION job.job_update
(
    id INT,
	title VARCHAR(200),
	starttime TIMESTAMP(0),
	endtime TIMESTAMP(0),
	estimate DECIMAL(9, 2),
	openassignment BOOLEAN,
	"description" TEXT,
	businessid INT,
	clientid INT,
	technicianid INT
)
RETURNS SETOF job.v_jobs
LANGUAGE SQL AS
$$

UPDATE
    job.jobs
SET
    title = title,
    startTime = starttime,
    endTime = endtime,
    estimate = estimate,
    openAssignment = openassignment,
    "description" = "description",
    businessId = businessid,
    clientId = clientid,
    technicianId = technicianid
WHERE
    id = id;

SELECT
	*
FROM
	job.v_jobs
WHERE
	id = id;
$$
