CREATE OR REPLACE VIEW business.v_businesses
AS

SELECT
    id,
    "name",
    addressline1,
    addressline2,
    city,
    "state",
    zipcode
FROM
    business.businesses;
