const { Pool } = require("pg");
const config = require("../config");
const pool = new Pool(config.db);

async function query(query, parameters) {
  const { rows } = await pool.query(query, parameters);
  return rows ? rows : [];
}

async function queryFunc(func, parameters) {
  const sql = "SELECT * FROM " + func;
  const rows = await query(sql, parameters);
}

async function call(func, parameters) {
  const sql = "CALL " + func;
  await query(sql, parameters);
}

module.exports = {
  query,
  queryFunc,
  call
};
