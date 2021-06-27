const express = require("express");
const hash = require("../services/hash");
const jwt = require("jsonwebtoken");
const router = express.Router();
const users = require("../services/users");

router.get("/", async (req, res) => {
  res.json({
    message: "test"
  }).end();
});

router.post("/", async (req, res) => {
  const { emailAddress, password } = req.body;
  if (!emailAddress) {
    res.status(400).json("Email address was not provided or was invalid.");
    return;
  }

  if (!password) {
    res.status(400).json("Password was not provided or was invalid.");
    return;
  }

  const user = await users.get(emailAddress);
  if (!user) {
    res.status(401).end();
    return;
  }

  const match = await hash.validateHash(password, user.password);
  if (!match) {
    res.status(401).json("Invalid password.");
    return;
  }

  const payload = {
    emailAddress: emailAddress,
    password: password
  };

  const token = jwt.sign(payload, config.app.secret);
  res.cookie("token", token, {
    httpOnly: true
  });

  // Don't return the password.
  delete user.password;

  res.status(200).send(user);
});

module.exports = router;