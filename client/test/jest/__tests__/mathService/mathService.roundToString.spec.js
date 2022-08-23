import { describe, expect, it } from "@jest/globals";

import { roundToString } from "src/services/mathService";

describe("roundToString input null or other strange stuff", () => {
  const cases = [
    [null],
    [undefined],
    [NaN],
    [[]],
    [[123]],
    ["5"],
    ["text"],
    [() => {}],
    [() => 5],
    ["🍕"],
  ];

  test.each(cases)("When input %p then ''", (input) => {
    const result = roundToString(input, 0);

    expect(result).toEqual("");
    expect(typeof result).toEqual("string");
  });
});

describe("roundToString digits null or other strange stuff", () => {
  const cases = [
    [-211],
    [-1],
    [1.2],
    [null],
    [undefined],
    [NaN],
    [[]],
    [[123]],
    ["5"],
    ["text"],
    [() => {}],
    [() => 5],
    ["🍕"],
  ];
  test.each(cases)("When digits %p then ''", (digits) => {
    const result = roundToString(123, digits);

    expect(result).toEqual("");
    expect(typeof result).toEqual("string");
  });
});

describe("roundToString to int", () => {
  const cases = [
    [10, "10"],
    [-23, "-23"],
    [-23.1, "-23"],
    [-23.5, "-24"],
    [-23.9, "-24"],
    [1.1, "1"],
    [1.5, "2"],
    [1.9, "2"],
  ];

  test.each(cases)(
    "When input %p and digits 0 then %p",
    (input, expectedResult) => {
      const result = roundToString(input, 0);

      expect(result).toEqual(expectedResult);
      expect(typeof result).toEqual("string");
    }
  );
});
