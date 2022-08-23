export function roundToString(number, digits) {
  if (number !== 0) {
    if (!number || typeof number !== "number") {
      return "";
    }
  }

  if (digits !== 0) {
    if (!digits || typeof digits !== "number" || digits < 0) {
      return "";
    }
  }

  if (Number.isInteger(digits) === false) {
    return "";
  }

  const rounded = number.toFixed(digits);
  return rounded.toString();
}
