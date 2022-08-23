import { describe, expect, it } from "@jest/globals";

import { roundToString } from "src/services/mathService";

const roundToIntCases = [
  [10, "10"],
  [-23, "-23"],
  [1.1, "1"],
  [1.5, "2"],
  [1.9, "2"],
];
describe("roundToString to int", () => {
  test.each(roundToIntCases)("When %p then %p", (input, expectedResult) => {
    const result = roundToString(input, 0);

    expect(result).toEqual(expectedResult);
    expect(typeof result).toEqual("string");
  });
});
