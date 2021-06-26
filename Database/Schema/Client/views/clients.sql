CREATE VIEW client.v_clients
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
    client.clients;
