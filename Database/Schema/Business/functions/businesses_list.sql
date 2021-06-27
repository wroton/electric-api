CREATE OR REPLACE FUNCTION business.businesses_list()
RETURNS TABLE (id INT)
LANGUAGE SQL AS
$$

SELECT
    id
FROM
    business.businesses;
$$
