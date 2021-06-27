const express = require("express");
const router = express.Router();
const administrators = require("../services/administrators");

router.get("/", async (req, res, next) => {
  const ids = await administrators.list();
  res.json(ids);
});

module.exports = router;