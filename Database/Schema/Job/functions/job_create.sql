CREATE OR REPLACE FUNCTION job.job_create
(
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

INSERT INTO job.jobs
	(title, starttime, endtime, estimate,
     openassignment, "description", businessid,
     clientid, technicianid)
VALUES
	(title, starttime, endtime, estimate,
     openassignment, "description", businessid,
     clientid, technicianid);

SELECT
	*
FROM
	job.v_jobs
WHERE
	id = LASTVAL();
$$

