CREATE OR REPLACE FUNCTION job.jobs_list
(
    userid INT
)
RETURNS TABLE (id INT)
LANGUAGE SQL AS
$$

SELECT
    j.id
FROM
    job.jobs AS j
    INNER JOIN "user".users AS u ON u.id = userid AND u.businessid = j.businessid
WHERE
    u.id = userid;
$$;
