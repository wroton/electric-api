CREATE OR REPLACE FUNCTION job.jobs_resolve
(
    ids INT[]
)
RETURNS SETOF job.v_jobs
LANGUAGE SQL AS
$$

SELECT
    j.*
FROM
    job.v_jobs AS j
    INNER JOIN UNNEST(ids) AS i ON i = j.id;
$$;
