function fromDb(i) {
  if (!i) {
    throw Error("Could not build user.");
  }

  if (typeof i !== "Object") {
    throw Error("Could not build user.");
  }

  return {
    id: i.id,
    emailAddress: i.emailaddress,
    password: i.password,
    businesssId: i.businessid
  };
}

module.exports = {
  fromDb
};