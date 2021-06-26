CREATE VIEW job.v_jobs
AS

SELECT
    id,
    title,
    starttime,
    endtime,
    estimate,
    openassignment,
    "description",
    businessid,
    clientid,
    technicianid
FROM
    job.jobs;
