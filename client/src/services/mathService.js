// todo: add tests

export function roundToString(number, digits) {
  if (!number || typeof number !== "number") {
    return "";
  }

  const rounded = number.toFixed(0);
  return rounded.toString();
}
