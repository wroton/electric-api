const config = {
  app: {
    host: "0.0.0.0",
    port: 3000,
    secret: "secret"
  },
  db: {
    host: "172.18.0.2",
    port: 5432,
    user: "postgres",
    password: "postgres",
    database: "postgres"
  }
};

module.exports = config;