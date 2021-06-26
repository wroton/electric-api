CREATE VIEW technician.v_technicians
AS

SELECT
    id,
    "name",
    positionid,
    businessid,
    userid
FROM
    technician.technicians;
