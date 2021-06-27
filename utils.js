const bcrypt = require("bcrypt");

// Hashes a value.
async function hash(value) {
  // Value must be populated.
  if (!value) {
    return false;
  }

  try {
    // Perform the salt.
    return await bcrypt.hash(value, 10);
  } catch (err) {
    return false;
  }
}

// Validates a value against its hash.
async function validateHash(providedValue, correctValue) {
  // Values must be populated.
  if (!providedValue || !correctValue) {
    return false;
  }

  try {
    const t = await bcrypt.compare(providedValue, correctValue);
    return t;
  } catch (err) {
    return false;
  }
}

module.exports = {
  hash: hash,
  validateHash: validateHash
};