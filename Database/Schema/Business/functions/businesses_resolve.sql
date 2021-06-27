CREATE OR REPLACE FUNCTION business.businesses_resolve
(
    ids INT[]
)
RETURNS SETOF business.v_businesses
LANGUAGE SQL AS
$$

SELECT
    b.*
FROM
    business.v_businesses AS b
    INNER JOIN UNNEST(ids) AS i ON i = b.id;
$$;
