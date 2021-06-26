CREATE OR REPLACE FUNCTION technician.positions_list
(
    userid INT
)
RETURNS TABLE (id INT)
LANGUAGE SQL AS
$$

SELECT
	p.id
FROM
	technician.v_positions AS p
    INNER JOIN "user".users AS u ON u.businessid = p.businessid
WHERE
    u.id = userid;
$$
