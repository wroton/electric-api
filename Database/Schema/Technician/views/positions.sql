CREATE OR REPLACE VIEW technician.v_positions
AS

SELECT
    id,
    "name",
    businessid
FROM
    technician.positions;
