const db = require("./db");

async function list() {
  const rows = await db.queryFunc(
    "administrators.administrators_list"
  );

  return rows;
}

module.exports = {
  list
};