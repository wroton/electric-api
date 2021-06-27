const bodyParser = require("body-parser");
const cookieParser = require("cookie-parser");
const express = require("express");
const jwt = require("jsonwebtoken");
const utils = require("./utils.js");

// Routes.
const administratorsRouter = require("./routes/administrators");
const businessesRouter = require("./routes/businesses");
const clientsRouter = require("./routes/clients");
const jobsRouter = require("./routes/jobs");
const techniciansRouter = require("./routes/technicians");
const tokensRouter = require("./routes/tokens");
const usersRouter = require("./routes/users");

// Settings.
const PORT = 3000;
const HOST = "0.0.0.0";
const DBSERVER = "";

// Setup the Express instance.
const app = express();

app.use(express.urlencoded({
  extended: true
}));

app.use(express.json());

// Always determine the current time in UTC.
app.use(function (req, res, next) {
  // Detemrine UTC now.
  const utcNow = new Date(new Date().toUTCString());

  // Set the value.
  req.utcNow = utcNow;

  // Continue.
  next();
});

// Setup routes.
app.use("/administrators", administratorsRouter);
app.use("/businesses", businessesRouter);
app.use("/clients", clientsRouter);
app.use("/jobs", jobsRouter);
app.use("/technicians", techniciansRouter);
app.use("/tokens", tokensRouter);
app.use("/users", usersRouter);

app.listen(PORT, HOST, err => {
  if (err) {
    console.log(err);
    return;
  }

  console.log("Listening on port " + PORT + ".");
});