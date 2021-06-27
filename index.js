const cookieParser = require("cookie-parser");
const config = require("./config");
const cors = require("cors");
const express = require("express");
const securer = require("./services/securer");

// Routes.
const administratorsRouter = require("./routes/administrators");
const businessesRouter = require("./routes/businesses");
const clientsRouter = require("./routes/clients");
const jobsRouter = require("./routes/jobs");
const techniciansRouter = require("./routes/technicians");
const tokensRouter = require("./routes/tokens");
const usersRouter = require("./routes/users");

// Setup the Express instance.
const app = express();

app.use(express.urlencoded({
  extended: true
}));

app.use(express.json());

// CORS.
app.use(cors({
  credentials: true,
  origin: "*"
}));
app.options("*", cors());

// Always determine the current time in UTC.
app.use(function (req, res, next) {
  // Detemrine UTC now.
  const utcNow = new Date(new Date().toUTCString());

  // Set the value.
  req.utcNow = utcNow;

  // Continue.
  next();
});

// Skip auth on some routes.
app.use(function (req, res, next) {
  req.skipAuth = req.url === "/api/1/tokens";
  next();
});

// Setup anonymous routes.
app.use("/api/1/tokens", tokensRouter);

// Setup token checking.
app.use(cookieParser());
app.use(securer);

// Setup authorized routes.
app.use("/api/1/administrators", administratorsRouter);
app.use("/api/1/businesses", businessesRouter);
app.use("/api/1/clients", clientsRouter);
app.use("/api/1/jobs", jobsRouter);
app.use("/api/1/technicians", techniciansRouter);
app.use("/api/1/users", usersRouter);

app.listen(config.app.port, config.app.host, err => {
  if (err) {
    console.log(err);
    return;
  }

  console.log("Listening on port " + config.app.port + ".");
});
