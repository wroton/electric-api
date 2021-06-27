CREATE OR REPLACE FUNCTION administrator.administrators_list
(
    userid INT
)
RETURNS TABLE (id INT)
LANGUAGE SQL AS
$$

SELECT
    a.id
FROM
    administrator.v_administrators AS a
    INNER JOIN "user".users AS u ON u.id = a.userid
WHERE
    u.businessid IS NULL OR u.businessid = a.businessid;
$$;
