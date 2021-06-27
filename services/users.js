const db = require("./db");
const { fromDb } = require("../models/user");

async function get(emailAddress) {
  const rows = await db.query(
    'SELECT * FROM "user".v_users WHERE emailaddress = $0 LIMIT 1;',
    [emailAddress]
  );

  if (!rows) {
    return [];
  }

  if (!rows[0]) {
    return undefined;
  }

  const user = fromDb(rows[0]);
  return user;
}

module.exports = {
  get
};