const config = require("../config");
const jwt = require("jsonwebtoken");
const users = require("./users");

function destroyCookie(res) {
  res.cookie("token", "1", {
    maxAge: 1,
    httpOnly: true
  });
}

module.exports = async (req, res, next) => {
  try {
    // Get the token.
    const token = req.cookies.token;
    if (!token) {
      res.status(401).json("No token provided.");
      return;
    }

    // Verify the token.
    const verifiedToken = await jwt.verify(token, config.app.secret);

    // Get the user.
    const user = await users.get(verifiedToken.emailAddress);
    if (!user) {
      destroyCookie(res);
      res.status(401).end();
      return;
    }

    // Check password.
    const match = verifiedToken.password === user.password;
    if (!match) {
      res.status(401).end();
      destroyCookie(res);
      return;
    }

    // Set the user on the request for convenience.
    req.user = user;
  } catch (err) {
    destroyCookie(res);

    res.status(401).end();
    return;
  }

  next();
};